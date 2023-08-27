using UnityEngine;
using Zenject;

namespace Assets.Game.Intro
{
    class InputIntroController : MonoBehaviour
    {
        private IntroManager introManager;
        [Inject]
        private void Construct(IntroManager introManager)
        {
            this.introManager = introManager;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
            {
                introManager.SkipIntroStep();
            }
        }

        public class Factory : PlaceholderFactory<InputIntroController>
        {
        }
    }
}
