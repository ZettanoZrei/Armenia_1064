using Assets.Modules;
using System.Threading.Tasks;

namespace Assets.Game.HappeningSystem
{
    class LeaveCampAfterAction : IAfterHappenAction
    {
        private readonly SetupCampManager setupCampManager;
        private readonly MySceneManager sceneManager;
        private ICallBack afterActionManagerCallBack;
        public LeaveCampAfterAction(SetupCampManager setupCampManager, MySceneManager sceneManager)
        {
            this.setupCampManager = setupCampManager;
            this.sceneManager = sceneManager;

            sceneManager.OnChangeScene_Post += CallBack;
        }

        private void CallBack(Scene scene)
        {
            if (sceneManager.IsTravelScene(scene))
            {
                sceneManager.OnChangeScene_Post -= CallBack;
                afterActionManagerCallBack.Return(null);
            }
        }

        void IAfterHappenAction.Do(ICallBack callBack)
        {
            afterActionManagerCallBack = callBack;
            LeaveCampDelay();           
        }

        private async Task LeaveCampDelay()
        {
            await Task.Delay(100);
            setupCampManager.LeaveCamp();
        }

        public IAfterHappenAction Clone()
        {
            return (IAfterHappenAction)MemberwiseClone();
        }
    }
}
