using Assets.Modules;

namespace Assets.Game.HappeningSystem
{
    class ActivateHappeningAfterAction : IAfterHappenAction
    {
        private readonly HappeningManager happeningManager;
        private readonly string questTitle;
        public ActivateHappeningAfterAction(HappeningManager happeningManager, string questTitle)
        {
            this.happeningManager = happeningManager;
            this.questTitle = questTitle;
        }

        void IAfterHappenAction.Do(ICallBack callBack)
        {
            this.happeningManager.PutHappeningToQueue(questTitle);
        }

        public IAfterHappenAction Clone()
        {
            return (IAfterHappenAction)MemberwiseClone();
        }
    }
}
