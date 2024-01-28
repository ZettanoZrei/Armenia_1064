using Assets.Game.Configurations;
using Assets.Game.HappeningSystem.Happenings;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.HappeningSystem.View.Common;
using Assets.Game.HappeningSystem.View.Dialog;
using Assets.Modules;
using Entities;
using GameSystems.Modules;
using Model.Entities.Answers;
using Model.Entities.Nodes;
using Model.Entities.Persons;
using Model.Entities.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class DialogPresenter : IInitializable, 
        IGameReadyElement, 
        IGameStartElement
    {
        private DialogView dialogView;
        private DialogModelDecorator dialogModelDecorator;
        private PortraitButton.Factory factory;
        private DialogPersonPackCatalog personPackCatalog;
        private RelationManager relationManager;
        private readonly SignalBus signalBus;
        private OtherConfig otherConfig;

        private UpdatePacket currentUpdatePacket;

        public DialogPresenter(DialogView dialogViewFacade, DialogModelDecorator dialogModelDecorator, OtherConfig otherConfig,
            PortraitButton.Factory factory, DialogPersonPackCatalog personPackCatalog, RelationManager relationManager, SignalBus signalBus)
        {
            this.dialogModelDecorator = dialogModelDecorator;
            this.dialogView = dialogViewFacade;
            this.factory = factory;
            this.personPackCatalog = personPackCatalog;
            this.relationManager = relationManager;
            this.signalBus = signalBus;
            this.otherConfig = otherConfig;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }

        void IGameReadyElement.ReadyGame()
        {
            Subscribe();
        }

        void IGameStartElement.StartGame()
        {
            this.dialogModelDecorator.Begin();
        }

        public void LoadStartData(DialogStartPack startPack)
        {
            try
            {
                dialogView.InitBackground(startPack.FrontImage, startPack.BackImage);
                dialogView.InitPersonModels(startPack.StartPersons);
                dialogView.FocusCameraForStart(startPack.FirstPhrase.PersonName, startPack.FirstPhrase.IsShowFace);
                dialogView.InitAdviserPosition(startPack.Advisor);
            }
            catch (Exception ex) { Logger.WriteLog(ex.ToString()); }
            
        }

        private void UpdateView(UpdatePacket updatePacket)
        {
            try
            {
                currentUpdatePacket = updatePacket;
                if (updatePacket.Phrase != null)
                {
                    dialogView.UpdateText(updatePacket.Phrase);
                    CheckApproachPerson(updatePacket.Phrase);
                    CheckFocusSpeakingPerson(updatePacket.Phrase);
                    SetPortret(updatePacket.Phrase);
                }

                if (updatePacket.Answers != null)
                {
                    dialogView.ShowCloseButton(false);
                    dialogView.ShowNextTextButton(false);
                    dialogView.UpdateOptions(updatePacket.Answers);
                    if (updatePacket.IsFocus)
                    {
                        dialogView.FocusPerson(WorldState.Instance.Hero, updatePacket.IsShowFace);
                    }
                }
                else
                {
                    if (updatePacket.IsFinish)
                    {
                        dialogView.ShowCloseButton(true);
                        dialogView.ShowNextTextButton(false);
                    }
                    else
                    {
                        dialogView.ShowCloseButton(false);
                        dialogView.ShowNextTextButton(true);
                    }
                }

                if (updatePacket.Advices != null)
                {
                    var persons = updatePacket.Advices.Select(x => (x as AdvicePhrase).PersonName);
                    var portraits = CreatePortraits(persons);
                    dialogView.ShowAdvicePopup(portraits);
                }
            }
            catch (Exception ex) { Logger.WriteLog(ex.ToString()); }
        }

        public void PlayAdvice(PersonName name)
        {
            var advice = (AdvicePhrase)currentUpdatePacket.Advices.First(x => (x as AdvicePhrase).PersonName.Equals(name));
            dialogView.PlayAdvice(advice);
        }

        private List<PortraitButton> CreatePortraits(IEnumerable<PersonName> people)
        {
            var portraitButtons = new List<PortraitButton>();
            foreach (var name in people)
            {
                var portrait = factory.Create();
                var pack = personPackCatalog.GetPack(name.Name);
                var relationValue = relationManager.GetRelation(name.Name);
                portrait.SetPortrait(pack.Portret, name, relationValue, relationRestriction: otherConfig.adviceRelationRestriction);
                portraitButtons.Add(portrait);
            }
            return portraitButtons;
        }

        private void ListenMakeDecition(int answerIndex)
        {
            this.dialogModelDecorator.MoveNextNode(answerIndex);
            this.dialogView.CloseAdvicePopup();
        }

        private void InitApproachPerson(Person person)
        {
            this.dialogView.CreatePersonFigure(person);
        }

        private void CheckFocusSpeakingPerson(Phrase phrase)
        {
            var dialogPhrase = phrase as DialogPhrase;
            if (dialogPhrase.IsCameraShow || dialogPhrase.IsShowFace)
                dialogView.FocusPerson(dialogPhrase.PersonName, dialogPhrase.IsShowFace);
        }
        private void CheckApproachPerson(Phrase phrase)
        {
            dialogModelDecorator.CheckApproachPerson(phrase);
        }

        private void SetPortret(Phrase phrase)
        {
            var dialogPhrase = phrase as DialogPhrase;
            var relationValue = relationManager.GetRelation(dialogPhrase.PersonName.Name);
            dialogView.SetPortret(dialogPhrase.PersonName, relationValue);
        }

        private void SetHeroPortetForAnswer()//?
        {
            dialogView.SetHeroPortret(true);
        }


        private void Finish(HappeningModel _, AfterAction __)
        {
            dialogView.Finish();
            Unsubscribe();
        }
        private void Subscribe()
        {
            dialogView.OnDecitionMade += ListenMakeDecition;
            dialogView.OnClickPortrait += PlayAdvice;
            dialogView.OnClose += dialogModelDecorator.Finish;

            dialogModelDecorator.OnMoveNext += UpdateView;
            dialogModelDecorator.OnFinishHappeningModel += Finish;
            dialogModelDecorator.OnApproachPerson += InitApproachPerson;
            dialogModelDecorator.OnLoadedDialogStartPack += LoadStartData;
            dialogView.OnNextPhrase += dialogModelDecorator.MoveNextPhrase;
        }
        private void Unsubscribe()
        {
            dialogView.OnDecitionMade -= ListenMakeDecition;
            dialogView.OnClickPortrait -= PlayAdvice;
            dialogView.OnClose -= dialogModelDecorator.Finish;

            dialogModelDecorator.OnMoveNext -= UpdateView;
            dialogModelDecorator.OnFinishHappeningModel -= Finish;
            dialogModelDecorator.OnApproachPerson -= InitApproachPerson;
            dialogModelDecorator.OnLoadedDialogStartPack -= LoadStartData;

            dialogView.OnNextPhrase -= dialogModelDecorator.MoveNextPhrase;
        }

        
    }
}
