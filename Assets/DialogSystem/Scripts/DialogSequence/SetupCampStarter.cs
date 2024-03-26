using Assets.Modules;
using System.Threading.Tasks;
using Zenject;



namespace Assets.DialogSystem.Scripts
{
    public class SetupCampStarter : ISequenceAction
    {
        private readonly SetupCampManager setupCampManager;
        private readonly MySceneManager sceneManager;
        private readonly int dialogAvailable;
        private ICallBack sequenceManagerCallBack;
        public SetupCampStarter(SetupCampManager setupCampManager, MySceneManager sceneManager, int dialogAvailable)
        {
            this.setupCampManager = setupCampManager;
            this.sceneManager = sceneManager;
            this.dialogAvailable = dialogAvailable;
        }

        Task ISequenceAction.Execute(ICallBack callBack)
        {
            sequenceManagerCallBack = callBack;
            sceneManager.OnChangeScene_Post += CheckCamp;
            setupCampManager.SetupCamp(dialogAvailable);
            return Task.CompletedTask;
        }

        private void CheckCamp(Scene _)
        {
            sequenceManagerCallBack.Return(null);
        }

        public class Factory : PlaceholderFactory<int, SetupCampStarter>
        {
        }
    }
}
