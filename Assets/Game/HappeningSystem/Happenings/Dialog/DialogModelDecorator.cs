using Assets.Game.HappeningSystem.View.Common;
using Assets.Game.HappeningSystem.View.Dialog;
using Model.Entities.Answers;
using Model.Entities.Happenings;
using Model.Entities.Nodes;
using Model.Entities.Persons;
using Model.Entities.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem.Happenings
{
    class DialogModelDecorator
    {
        private HappeningModel model;
        public PopupType PopupType => model.PopupType;
        public string Title => model.Title;
        private List<Person> personComingDuringDialog = new List<Person>();
        private readonly DialogBackgroundKeeper backgroundManager;


        public event Action<HappeningModel, AfterAction> OnFinishHappeningModel
        {
            add { model.OnFinishHappeningModel += value; }
            remove { model.OnFinishHappeningModel -= value; }
        }
        public event Action<UpdatePacket> OnMoveNext
        {
            add { model.OnMoveNext += value; }
            remove { model.OnMoveNext -= value; }
        }

        public event Action OnShowAnswers
        {
            add { model.OnShowAnswers += value; }
            remove { model.OnShowAnswers -= value; }
        }

        public bool IsModel=> model != null;

        public event Action<Person> OnApproachPerson;
        public event Action<DialogStartPack> OnLoadedDialogStartPack;

        public DialogModelDecorator(DialogBackgroundKeeper backgroundManager)
        {
            this.backgroundManager = backgroundManager;
        }
        public void InitModel(HappeningModel model)
        {
            this.model = model;
        }

        public void Begin()
        {
            try
            {
                LoadStartPack();
                model.Begin();
            }
            catch (Exception ex) { Logger.WriteLog(ex.ToString()); }
        }

        public void Finish() => model.Finish();
        public void MoveNextNode(int answerNumber) => model.MoveNextNode(answerNumber);
        public void MoveNextPhrase() => model.MoveNextPhrase();

        public void LoadStartPack()
        {
            if (model.HappeningData is DialogHappening dialog)
            {
                var dialogStartPack = new DialogStartPack();
                InitBackground(dialog, dialogStartPack);
                InitPersonModel(dialog, dialogStartPack);
                dialogStartPack.Advisor = dialog.Advisor;
                dialogStartPack.FirstPhrase = model.HappeningData.Nodes[0].Phrases[0] as DialogPhrase;
                OnLoadedDialogStartPack?.Invoke(dialogStartPack);
            }
            else
            {
                Logger.WriteLog("Not a dialog!");
                throw new Exception("Not a dialog!");
            }

        }
        public void CheckApproachPerson(Phrase phrase)
        {
            var dialogPhrase = phrase as DialogPhrase;
            if (personComingDuringDialog.Any(x => x.Name.Equals(dialogPhrase.PersonName)))
            {
                var person = personComingDuringDialog.First(x => x.Name.Equals(dialogPhrase.PersonName));
                personComingDuringDialog.Remove(person);
                OnApproachPerson?.Invoke(person);
            }
        }

        private void InitBackground(DialogHappening dialog, DialogStartPack dialogStartPack)
        {
            var path = "Background/Dialog/";
            if (!dialog.IsSpecialBackground)
            {
                //Если бэк диалога oпределен случайным местом, а не квестом 
                dialogStartPack.FrontImage = Resources.Load<Sprite>(path + backgroundManager.DialogFront);
                dialogStartPack.BackImage = Resources.Load<Sprite>(path + backgroundManager.DialogBack);
            }
            else
            {
                //TODO check
                dialogStartPack.FrontImage = Resources.Load<Sprite>(path + dialog.FrontImageUri);
                dialogStartPack.BackImage = Resources.Load<Sprite>(path + dialog.BackImageUri);
            }
        }

        private void InitPersonModel(DialogHappening dialog, DialogStartPack dialogStartPack)
        {
            dialogStartPack.StartPersons = dialog.PeopleForDialog.Where(x => !x.IsApproached).ToList();
            personComingDuringDialog = dialog.PeopleForDialog.Except(dialogStartPack.StartPersons).ToList();
        }
    }
}
