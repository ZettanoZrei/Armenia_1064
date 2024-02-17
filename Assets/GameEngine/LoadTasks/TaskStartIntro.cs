using Assets.Game.Core;
using Assets.Game.Intro;
using System;
using Zenject;

namespace Loader
{
    class TaskStartIntro : IInitializable
    {
        private readonly IntroManager introManager;

        public TaskStartIntro(IntroManager introManager)
        {
            this.introManager = introManager;
        }

        void IInitializable.Initialize()
        {
            introManager.Begin();
        }
    }
}
