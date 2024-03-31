using Entities;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    //Запускается сразу при заходе в лагерь. Необходим приориет 3 для события
    class LaunchComeInCampQuest : IInitializable
    {
        async void IInitializable.Initialize()
        {
            await LaunchQuest();
        }

        private async Task LaunchQuest()
        {
            await Task.Delay(1000);
            var priority = 3;
            //happeningManager.LaunchHappenFromQueue(priority);
        }
    }
}
