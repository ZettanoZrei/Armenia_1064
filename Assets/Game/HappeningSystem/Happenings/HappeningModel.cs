using Assets.Game.HappeningSystem.AfterHappenAction;
using Assets.Game.HappeningSystem.Persons;
using Assets.Game.HappeningSystem.View.Common;
using Assets.Game.HappeningSystem.View.Dialog;
using Entities;
using GameSystems;
using Model.Entities.Answers;
using Model.Entities.Happenings;
using Model.Entities.Nodes;
using Model.Entities.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class HappeningModel
    {
        #region fields
        public PopupType PopupType { get; private set; }
        public string Title => happeningData.Title;
        public Happening HappeningData => happeningData;

        private RelationManager relationManager;
        private ConsequencesHandler consequencesHandler;
        private Happening happeningData;
        private AfterAction currentAfterAction = new AfterAction();

        public event Action<HappeningModel, AfterAction> OnFinishHappeningModel;
        public event Action OnShowAnswers; //for tutorial
        public event Action<UpdatePacket> OnMoveNext;

        private Node currentNode;
        private int phraseIndex = -1;
        private Answer currentEmptyAnswer;
        #endregion

        [Inject]
        public void Construct(RelationManager relationManager, ConsequencesHandler consequencesHandler)
        {
            this.relationManager = relationManager;
            this.consequencesHandler = consequencesHandler;
            consequencesHandler.Clear();
        }

        public void InitHappeningData(Happening happening)
        {
            this.happeningData = happening;
            DefinePopupType(happening);
        }


        public void Begin()
        {
            Logger.WriteLog($"Начало: квест - {happeningData.Quest}, событие - {happeningData.Title}");
            currentNode = GetNode(1);
            MoveNextPhrase();
        }

        //суть в том что я искуственно выполняю пустой ответ после последней фразы
        public void MoveNextPhrase()
        {
            if (currentEmptyAnswer != null)
            {
                MoveNextNode(0);
                currentEmptyAnswer = null;
                return;
            }
            phraseIndex++;
            TryСatchEmptyAnswer();

            var updatePack = GetUpdatePacket();
            if (updatePack.Answers != null)
                OnShowAnswers?.Invoke();
            OnMoveNext?.Invoke(updatePack);
        }

        public void MoveNextNode(int answerIndex)
        {
            //step 1
            phraseIndex = 0;
            var answer = FindAnswer(answerIndex);

            //step 2
            HandleAnswer(answer);

            //step 3
            this.currentNode = GetNode(answer.ToStep);
            if (currentNode == null)
            {
                OnMoveNext.Invoke(new UpdatePacket { IsFinish = true });
                return;
            }
            DefineAccessToAnswers(currentNode);

            //step 4
            var updatePack = GetUpdatePacket();
            OnMoveNext?.Invoke(updatePack);
            TryСatchEmptyAnswer();
        }

        private void TryСatchEmptyAnswer()
        {
            if (IsEmptyAnswerTime())
            {
                currentEmptyAnswer = currentNode.Answers[0];
            }
        }

        public void Finish()
        {
            if (currentEmptyAnswer != null)
                HandleAnswer(currentEmptyAnswer);

            //write down all consequences for message popup
            currentAfterAction.Consequences = consequencesHandler.GetConsequences();

            consequencesHandler.InvokeConsequencesEvents();
            OnFinishHappeningModel.Invoke(this, currentAfterAction);
        }


        private UpdatePacket GetUpdatePacket()
        {
            return new UpdatePacket
            {
                Phrase = GetNextPhrase(),
                IsFinish = GetIsFinish(),
                Answers = GetAnswers(),
                Advices = GetAdvices(),
            };
        }
        private Phrase GetNextPhrase()
        {
            return currentNode.Phrases[phraseIndex];
        }
        private List<Answer> GetAnswers()
        {
            if (!IsLastPhraseInNode() || AnalizeNode(currentNode) != NodeType.Usual)
                return null;

            return currentNode.Answers.Any() ? currentNode.Answers : null;
        }
        private bool IsEmptyAnswerTime()
        {
            return IsLastPhraseInNode() && AnalizeNode(currentNode) != NodeType.Usual;
        }
        private List<Phrase> GetAdvices()
        {
            if (!IsLastPhraseInNode())
                return null;

            return currentNode.Advices.Any() ? currentNode.Advices : null;
        }
        private bool GetIsFinish()
        {
            return IsLastPhraseInNode() && AnalizeNode(currentNode) == NodeType.EmptyToEnd;
        }
        private Answer FindAnswer(int answerIndex) => currentNode.Answers.First(x => x.Index == answerIndex);
        private bool IsLastPhraseInNode() => phraseIndex + 1 == currentNode.Phrases.Count;
        private void HandleAnswer(Answer answer)
        {
            SetAfterAction(answer.AfterAction);
            consequencesHandler.AccumulateConsequences(answer);
        }
        private NodeType AnalizeNode(Node node)
        {
            if (node == null)
                return NodeType.NonNode;
            if (node.Answers.Count == 1 && string.IsNullOrEmpty(node.Answers[0].Text))
            {
                if (node.Answers[0].ToStep == 0)
                    return NodeType.EmptyToEnd;
                else
                    return NodeType.EmptyToNode;
            }
            return NodeType.Usual;
        }

        private void DefinePopupType(Happening happening)
        {
            if (happening is DialogHappening)
            {
                PopupType = PopupType.DialogPopup;
            }
            else if (happening is StoryHappening)
            {
                PopupType = PopupType.StoryPopup;
            }
            else if (happening is Happening)
            {
                PopupType = PopupType.AccidentPopup;
            }
        }
        private void SetAfterAction(AfterAction nextAfterAction)
        {
            if (nextAfterAction.IsAfterAction)
                currentAfterAction.IsAfterAction = true;

            if (nextAfterAction.SetupCamp)
                currentAfterAction.SetupCamp = true;

            if (nextAfterAction.LeaveCamp)
                currentAfterAction.LeaveCamp = true;

            if (nextAfterAction.IsActivateQuest)
            {
                currentAfterAction.Quest = nextAfterAction.Quest;
                currentAfterAction.Immediately = nextAfterAction.Immediately;
                currentAfterAction.IsActivateQuest = true;
            }

            if (nextAfterAction.SetupCamp)
                currentAfterAction.DialogAvailable = nextAfterAction.DialogAvailable;
        }
        private Node GetNode(int num) => happeningData.Nodes.FirstOrDefault(x => x.Step == num);


        private void DefineAccessToAnswers(Node node)
        {
            if (node == null) return;
            foreach (var answer in currentNode.Answers.Where(x => x.IsRestriction))
            {
                var restrictionPerson = answer.Restriction.PersonName;
                var ourRelationWithHim = relationManager.GetRelation(restrictionPerson.Name);
                answer.IsRestriction = ourRelationWithHim >= answer.Restriction.Value;
            }

        }
    }

    public enum NodeType
    {
        Usual,
        EmptyToNode,
        EmptyToEnd,
        NonNode
    }
}


