using Assets.Systems.SaveSystem;
using UnityEngine;
using Zenject;

namespace Assets.Game.UI.DebugLoading
{
    public class LoadGameButtonFactrory : MonoBehaviour
    {
        private SaveHelper<SaveData> saveHelper;
        public LoadGameButton prefab;
        LoadGameButton.Factory factory;

        [Inject]
        public void Contruct(SaveHelper<SaveData> saveHelper, LoadGameButton.Factory factory)
        {
            this.saveHelper = saveHelper;
            this.factory = factory;
        }

        private void Start()
        {
            var files = saveHelper.GetSavesFiles();
            foreach (var file in files)
            {
                var btn = factory.Create();
                btn.SetPath(file.CreationTime, file.FullName);
            }
        }
    }
}
