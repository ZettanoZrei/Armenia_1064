using Zenject;

namespace Assets.Game
{
    class DialogBackgroundController : IInitializable, ILateDisposable
    {
        private readonly SetupCampManager setupCampManager;
        private readonly DialogBackgroundKeeper dialogBackgroundKeeper;

        public DialogBackgroundController(SetupCampManager setupCampManager, DialogBackgroundKeeper dialogBackgroundKeeper)
        {
            this.setupCampManager = setupCampManager;
            this.dialogBackgroundKeeper = dialogBackgroundKeeper;
        }

        void IInitializable.Initialize()
        {
            setupCampManager.OnSetupCamp_Before += ChangeDialogBackground;
            setupCampManager.OnLeaveCamp_Before += ChangeDialogBackground2;
        }

        void ILateDisposable.LateDispose()
        {
            setupCampManager.OnSetupCamp_Before -= ChangeDialogBackground;
            setupCampManager.OnLeaveCamp_Before -= ChangeDialogBackground2;
        }

        private void ChangeDialogBackground()
        {
            dialogBackgroundKeeper.ChangeState(DialogBackgroundKeeper.BackgroundState.Camp);
        }
        private void ChangeDialogBackground2()
        {
            dialogBackgroundKeeper.ChangeState(DialogBackgroundKeeper.BackgroundState.Travel);
        }
    }
}
