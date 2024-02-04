using Assets.Modules;
using GameSystems.Modules;
using Model.Types;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Parameters.EndedParamSystem
{
    class EndedParamController : IInitializable, 
        ISceneReady, 
        ISceneFinish
    {
        private ParameterEndedObserver endedObserver;
        private PopupManager popupManager;
        private MySceneManager sceneManager;
        private EndedParamMechanics endedParamMechanics;
        private readonly SignalBus signalBus;

        public EndedParamController(ParameterEndedObserver endedObserver, PopupManager popupManager, MySceneManager sceneManager, EndedParamMechanics endedParamMechanics,
            SignalBus signalBus)
        {
            this.endedObserver = endedObserver;
            this.popupManager = popupManager;
            this.sceneManager = sceneManager;
            this.endedParamMechanics = endedParamMechanics;
            this.signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            signalBus.Fire(new ConnectGameElementEvent { GameElement = this });
        }
        void ISceneReady.ReadyScene()
        {
            endedObserver.OnParamZero += BeginPeopleRemoveTimer;
            endedObserver.OnParamNonZero += FinishPeopleRemoveTimer;
            popupManager.OnPopupChanged += CheckPopup;
            sceneManager.OnChangeScene_Post += CheckScene;
        }

        void ISceneFinish.FinishScene()
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
