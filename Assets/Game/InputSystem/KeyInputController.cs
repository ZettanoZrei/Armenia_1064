using Assets.Game.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Game.InputSystem
{
    internal class KeyInputController : IInitializable, ILateDisposable
    {
        private readonly MenuManager menuManager;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public KeyInputController(MenuManager menuManager)
        {
            this.menuManager = menuManager;
        }
        
        void IInitializable.Initialize()
        {
            Subscribe();
        }

        void ILateDisposable.LateDispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(_ => 
                {
                    if(!menuManager.IsActive)
                        menuManager.ShowMenu();
                    else
                        menuManager.HideMenu();
                })
                .AddTo(disposables);
        }

        private void Unsubscribe()
        {
            disposables.Clear();
        }
    }
}
