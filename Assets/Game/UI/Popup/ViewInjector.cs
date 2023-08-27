using Entities;
using GameSystems;
using UnityEngine;
using Zenject;

namespace Assets.Game
{
    public class ViewInjector : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform popupContainer;
        [SerializeField] private GameObject blockCurtain;
        private PopupManager popupManager;
        private PopupFabrica popupFabrica;

        [Inject]
        public void Construct(PopupManager popupManager, PopupFabrica popupFabrica) //block currtain должен инжектирвоаться раньше Initialize
        {
            this.popupManager = popupManager;
            this.popupFabrica = popupFabrica;
        }

        void IInitializable.Initialize()
        {
            popupFabrica.InjectPopupContainer(popupContainer);
            popupManager.InjectBlockCurtain(blockCurtain);
        }
    }
}
