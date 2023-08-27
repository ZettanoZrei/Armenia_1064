using Assets.Game.Configurations;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.HappeningSystem.View.Advice;
using Assets.Game.HappeningSystem.View.Common;
using Entities;
using GameSystems;
using Model.Entities.Answers;
using Model.Entities.Nodes;
using Model.Entities.Persons;
using Model.Entities.Phrases;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class AccidentPresenter
    {
        private AccidentView accidentView;
        private HappeningModel happeningModel;
        private PortraitButton.Factory factory;
        private DialogPersonPackCatalog personPackCatalog;
        private readonly PortaitHeap portaitHeap;
        private readonly RelationManager relationManager;
        private readonly OtherConfig otherConfig;

        private UpdatePacket currentUpdatePacket;
        public AccidentPresenter(PortraitButton.Factory factory, DialogPersonPackCatalog personPackCatalog, PortaitHeap portaitHeap,
            RelationManager relationManager, OtherConfig otherConfig)
        {
            this.factory = factory;
            this.personPackCatalog = personPackCatalog;
            this.portaitHeap = portaitHeap;
            this.relationManager = relationManager;
            this.otherConfig = otherConfig;
        }

        public void Init(HappeningModel happeningModel, AccidentView accidentView)
        {
            this.happeningModel = happeningModel;
            this.accidentView = accidentView;
            accidentView.InitPortraitHeap(portaitHeap);
            Subscribe();
        }


        public void Begin()
        {           
            happeningModel.Begin();
        }

        private void UpdateView(UpdatePacket updatePacket)
        {
            currentUpdatePacket = updatePacket; 
            if (updatePacket.Phrase != null)
                accidentView.UpdateText(updatePacket.Phrase);

            if(updatePacket.Answers != null)
            {
                accidentView.UpdateOptions(updatePacket.Answers);
            }               
            else
            {
                if(updatePacket.IsFinish) 
                {
                    accidentView.ShowCloseButton(true);
                    accidentView.ShowNextTextButton(false);
                }
                else
                {
                    accidentView.ShowCloseButton(false);
                    accidentView.ShowNextTextButton(true);
                }
            }

            if(updatePacket.Advices != null)
            {
                var persons = updatePacket.Advices.Select(x => (x as AdvicePhrase).PersonName);
                var portraits = CreatePortraits(persons);
                accidentView.ShowAdvicePopup(portraits);
            }
        }

        private void ListenMakeDecition(int answerIndex)
        {
            happeningModel.MoveNextNode(answerIndex);
            accidentView.HideAdvicePopup();
        }


        public void PlayAdvice(PersonName name)
        {
            var advice = (AdvicePhrase)currentUpdatePacket.Advices.First(x => (x as AdvicePhrase).PersonName.Equals(name));
            accidentView.UpdateText(advice);
            accidentView.HideAdvicePopup();
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


        private void Finish(HappeningModel _, AfterAction __)
        {
            currentUpdatePacket = null;
            accidentView.Finish();
            Unsubscribe();
        }
        private void Subscribe()
        {
            happeningModel.OnMoveNext += UpdateView;
            happeningModel.OnFinishHappeningModel += Finish;
            accidentView.OnDecitionMade += ListenMakeDecition;
            accidentView.OnClickPortrait += PlayAdvice;
            accidentView.OnClose += happeningModel.Finish;
            accidentView.OnNextPhrase += happeningModel.MoveNextPhrase;
        }
        private void Unsubscribe()
        {
            accidentView.OnClickPortrait -= PlayAdvice;
            accidentView.OnDecitionMade -= ListenMakeDecition;
            happeningModel.OnMoveNext -= UpdateView;
            happeningModel.OnFinishHappeningModel -= Finish;
            accidentView.OnClose -= happeningModel.Finish;
            accidentView.OnNextPhrase -= happeningModel.MoveNextPhrase;
        }
    }
}
