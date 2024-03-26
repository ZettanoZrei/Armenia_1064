using Assets.Game.HappeningSystem;
using Assets.Modules;
using ExtraInjection;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;

namespace Assets.DialogSystem.Scripts
{
    //управляет последовательностью действий после завершения диалога
    public class DialogSequenceManager : ICallBack, IInitializable, ILateDisposable, IExtraInject
    {
        private readonly Queue<Func<ISequenceAction>> actions = new Queue<Func<ISequenceAction>>();
        private bool isWorking;

        [ExtraInject] private DialogStarter.Factory dialogStarterFactory;
        [ExtraInject] private LeaveCampStarter.Factory leaveCampStarter;
        [ExtraInject] private SetupCampStarter.Factory setupCampStarter;


        void IInitializable.Initialize()
        {
            Lua.RegisterFunction(nameof(StartDialog), this, SymbolExtensions.GetMethodInfo(() => StartDialog(string.Empty, string.Empty)));
            Lua.RegisterFunction(nameof(SetupCamp), this, SymbolExtensions.GetMethodInfo(() => SetupCamp(default(double))));
            Lua.RegisterFunction(nameof(LeaveCamp), this, SymbolExtensions.GetMethodInfo(() => LeaveCamp()));
        }
        void ILateDisposable.LateDispose()
        {
            Lua.UnregisterFunction(nameof(StartDialog));
            Lua.UnregisterFunction(nameof(SetupCamp));
            Lua.UnregisterFunction(nameof(LeaveCamp));
        }

        public void StartDialog(string title, string actor)
        {
            actions.Enqueue(() => dialogStarterFactory.Create(title, actor));
        }

        public void SetupCamp(double dialogsAvailable)
        {
            actions.Enqueue(() => setupCampStarter.Create((int)dialogsAvailable));
        }

        public void LeaveCamp()
        {
            actions.Enqueue(() => leaveCampStarter.Create());
        }

        public void ExecuteSequence()
        {
            if (isWorking) //todo: нехотелось бы запускать это после каждого диалога, все это костыльно
            {
                Debug.LogWarning("Попытка запустить уже запущенный DialogSequenceManager");
                return;
            }
            isWorking = true;
            (this as ICallBack).Return(null);
        }

        void ICallBack.Return(object _)
        {
            if (!actions.Any())
            {
                isWorking = false;
                return;
            }
            var action = actions.Dequeue().Invoke();
            action.Execute(this);
        }
    }
}
