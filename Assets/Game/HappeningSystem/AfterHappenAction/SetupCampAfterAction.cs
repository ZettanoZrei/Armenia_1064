using Assets.Game.Camp;
using Assets.Modules;
using GameSystems;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class SetupCampAfterAction : IAfterHappenAction
    {
        private readonly SetupCampManager setupCampManager;
        private readonly MySceneManager sceneManager;
        private readonly int dialogAvailable;
        private ICallBack afterActionManagerCallBack;
        public SetupCampAfterAction(SetupCampManager setupCampManager, MySceneManager sceneManager, int dialogAvailable)
        {
            this.setupCampManager = setupCampManager;
            this.sceneManager = sceneManager;
            this.dialogAvailable = dialogAvailable;
            sceneManager.OnChangeScene_Post += CallBack;
        }

        private void CallBack(Scene scene)
        {
            if(scene == Scene.CampScene)
            {
                sceneManager.OnChangeScene_Post -= CallBack;
                afterActionManagerCallBack.Return(null);
            }
        }

        void IAfterHappenAction.Do(ICallBack callBack)
        {
            afterActionManagerCallBack = callBack;
            setupCampManager.SetupCamp(dialogAvailable);
        }

        public IAfterHappenAction Clone()
        {
            return (IAfterHappenAction)MemberwiseClone();
        }
    }
}
