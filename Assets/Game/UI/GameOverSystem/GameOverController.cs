using Assets.Game.Parameters;
using System.Linq;
using UniRx;
using Zenject;

namespace Assets.Game.UI.FailGameSystem
{
    class GameOverController : IInitializable, ILateDisposable
    {
        private readonly GameOverManager gameOverManager;
        private readonly ParametersManager parametersManager;
        CompositeDisposable disposables = new CompositeDisposable();
        public GameOverController(GameOverManager gameOverManager, ParametersManager parametersManager)
        {
            this.gameOverManager = gameOverManager;
            this.parametersManager = parametersManager;
        }

        void IInitializable.Initialize()
        {
            parametersManager.People
                .Where(x => x <= 0)
                //.Skip(1)
                .Subscribe(_ =>
                {
                    disposables.Clear();
                    gameOverManager.GameOver();
                })
                .AddTo(disposables);
        }

        void ILateDisposable.LateDispose()
        {
            disposables.Clear();
        }
    }
}
