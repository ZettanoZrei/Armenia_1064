using Assets.Modules;
using System.Threading.Tasks;
using Zenject;



namespace Assets.DialogSystem.Scripts
{
    public class LeaveCampStarter : ISequenceAction
    {
        private readonly SetupCampManager setupCampManager;
        private readonly MySceneManager sceneManager;
        private ICallBack sequenceManagerCallBack;
        public LeaveCampStarter(SetupCampManager setupCampManager, MySceneManager sceneManager)
        {
            this.setupCampManager = setupCampManager;
            this.sceneManager = sceneManager;
        }

        Task ISequenceAction.Execute(ICallBack callBack)
        {
            sequenceManagerCallBack = callBack;
            sceneManager.OnChangeScene_Post += CheckLeaveCamp;
            setupCampManager.LeaveCamp();
            return Task.CompletedTask;
        }

        private void CheckLeaveCamp(Scene _)
        {
            sequenceManagerCallBack.Return(null);
        }
        public class Factory : PlaceholderFactory<LeaveCampStarter>
        {
        }
    }
}
