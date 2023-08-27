using Assets.Game.Timer;
using GameSystems;
using UnityEngine;
using Zenject;

namespace Assets.Game.UI.TimeUI
{
    class TimeAdapter : MonoBehaviour, 
        IGameReadyElement, IGameFinishElement, IGameStartElement, IGameChangeSceneElement  
    {
        [Inject] private TimeMechanics model;
        [SerializeField] private TimeView view;

        void IGameReadyElement.ReadyGame()
        {
            model.OnTimeChanged += view.SetTime;
            model.OnDayChanged += view.SetDay;
        }       

        void IGameStartElement.StartGame()
        {
            view.SetDay(model.Days);
            view.SetTime(model.Time);
        }

        void IGameFinishElement.FinishGame()
        {
            model.OnTimeChanged -= view.SetTime;
            model.OnDayChanged -= view.SetDay;
        }

        void IGameChangeSceneElement.ChangeScene()
        {
            model.OnTimeChanged -= view.SetTime;
            model.OnDayChanged -= view.SetDay;
        }
    }
}
