using GameSystems;
using Model.Types;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Parameters.EndedParamSystem
{
    class EndedParamController : MonoBehaviour, IGameReadyElement, IGameFinishElement, IGameChangeSceneElement
    {
        private ParameterEndedObserver endedObserver;
        private PopupManager popupManager;
        private MySceneManager sceneManager;
        private EndedParamMechanics endedParamMechanics;

        [Inject]
        public void Construct(ParameterEndedObserver endedObserver, PopupManager popupManager, MySceneManager sceneManager, EndedParamMechanics endedParamMechanics)
        {
            this.endedObserver = endedObserver;
            this.popupManager = popupManager;
            this.sceneManager = sceneManager;
            this.endedParamMechanics = endedParamMechanics;
        }
        void IGameReadyElement.ReadyGame()
        {
            endedObserver.OnParamZero += BeginPeopleRemoveTimer;
            endedObserver.OnParamNonZero += FinishPeopleRemoveTimer;
            popupManager.OnPopupChanged += CheckPopup;
            sceneManager.OnChangeScene_Post += CheckScene;
        }

        void IGameFinishElement.FinishGame()
        {
            endedObserver.OnParamZero -= BeginPeopleRemoveTimer;
            endedObserver.OnParamNonZero -= FinishPeopleRemoveTimer;
            popupManager.OnPopupChanged -= CheckPopup;
            sceneManager.OnChangeScene_Post -= CheckScene;
        }
        void IGameChangeSceneElement.ChangeScene()
        {
            endedObserver.OnParamZero -= BeginPeopleRemoveTimer;
            endedObserver.OnParamNonZero -= FinishPeopleRemoveTimer;
            popupManager.OnPopupChanged -= CheckPopup;
            sceneManager.OnChangeScene_Post -= CheckScene;
        }

        private void CheckScene(Scene scene)
        {
            if(sceneManager.IsTravelScene(scene))
            {
                endedParamMechanics.ResumeTimers();
            }
            else
            {
                endedParamMechanics.StopTimers();
            }
        }

        private void CheckPopup(object sender, PopupManager.PopupChangedEventArgs e)
        {
            if(popupManager.CheckActivePausePopups() || !sceneManager.IsTravelScene(sceneManager.CurrentScene))
            {
                endedParamMechanics.StopTimers();
            }
            else
            {
                endedParamMechanics.ResumeTimers();
            }
        }

        private void BeginPeopleRemoveTimer(ParameterType parameterType)
        {
            endedParamMechanics.BeginTimer(parameterType);
        }

        private void FinishPeopleRemoveTimer(ParameterType parameterType)
        {
            endedParamMechanics.FinishTimer(parameterType);
        }

        
    }
}
