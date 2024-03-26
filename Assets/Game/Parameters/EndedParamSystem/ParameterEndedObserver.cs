using Model.Types;
using System;
using System.Security.Cryptography;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Parameters
{
    public class ParameterEndedObserver : IInitializable, ILateDisposable
    {
        public event Action<ParameterType> OnParamZero;
        public event Action<ParameterType> OnParamNonZero;

        private bool isFoodZero;
        private bool isStaminaZero;
        private bool isSpiritZero;

        private readonly ParametersManager parametersManager;

        private CompositeDisposable disposable = new CompositeDisposable();
        public ParameterEndedObserver(ParametersManager parametersManager)
        {
            this.parametersManager = parametersManager;
        }
        public void CheckFoodChanges(float stamina)
        {
            if (!isFoodZero && stamina == 0)
            {
                OnParamZero?.Invoke(ParameterType.Food);
                isFoodZero = true;
            }

            if (isFoodZero && stamina != 0)
            {
                OnParamNonZero?.Invoke(ParameterType.Food);
                isFoodZero = false;
            }
        }

        public void CheckSpiritChanges(float spirit)
        {
            if (!isSpiritZero && spirit == 0)
            {
                OnParamZero?.Invoke(ParameterType.Spirit);
                isSpiritZero = true;
                //Debug.Log("OnParamZero");
            }

            if (isSpiritZero && spirit != 0)
            {
                OnParamNonZero?.Invoke(ParameterType.Spirit);
                isSpiritZero = false;
                //Debug.Log("OnParamNonZero");
            }
        }

        public void CheckStaminaChanges(float stamina)
        {
            if (!isStaminaZero && stamina == 0)
            {
                OnParamZero?.Invoke(ParameterType.Stamina);
                isStaminaZero = true;
            }

            if (isStaminaZero && stamina != 0)
            {
                OnParamNonZero?.Invoke(ParameterType.Stamina);
                isStaminaZero = false;
            }
        }

        void IInitializable.Initialize()
        {
            parametersManager.Spirit.Subscribe(_ => CheckSpiritChanges(_)).AddTo(disposable);
            parametersManager.Food.Subscribe(_ => CheckFoodChanges(_)).AddTo(disposable);
            parametersManager.Stamina.Subscribe(_ => CheckStaminaChanges(_)).AddTo(disposable);
        }

        void ILateDisposable.LateDispose()
        {
            disposable.Clear();
        }
    }
}
