using Zenject;

namespace Assets.Game.Camp
{
    //для помощи в блокировки кнопки в нужные моменты
    class RestLocker : IInitializable, ILateDisposable
    {
        private readonly MySceneManager sceneManager;
        public bool IsRestOnceBlock { get; set; }
        public RestLocker(MySceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        void IInitializable.Initialize()
        {
            sceneManager.OnToTravelScene += BlockRest;
        }

        void ILateDisposable.LateDispose()
        {
            sceneManager.OnToTravelScene -= BlockRest;
        }

        private void BlockRest()
        {
            IsRestOnceBlock = false;
        }
    }
}
