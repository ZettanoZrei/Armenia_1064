using Assets.Systems.SaveSystem;
using Model.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.GameEngine.TEST_SYSTEMS
{
    public class TEST_SAVE_SYSTEM : MonoBehaviour
    {
        private SaveManager saveManager;
        private Repository repository;
        private LoadManager loadManager;
        private bool result;
        public bool Result => result;

        [Inject]
        public void Construct(SaveManager saveManager, LoadManager loadManager, Repository repository)
        {
            this.saveManager = saveManager;
            this.repository = repository;
            this.loadManager = loadManager;
        }
        public async void Save()
        {
            await saveManager.SaveAsync();
        }

        public async void DoTest()
        {
            await saveManager.SaveAsync();
            var origin = repository.SaveData;

            loadManager.LoadData();
            //repository.LoadSaveData();
            //var loaded = repository.SaveData;

            saveManager.FillSaveData();
            var loaded = repository.SaveData;

            result = Compare(origin, loaded);

        }

        private bool Compare(SaveData origin, SaveData loaded)
        {
            var c1 = Enumerable.SequenceEqual(origin.Quests, loaded.Quests);
            var c2 = Enumerable.SequenceEqual(origin.CaravanPositions, loaded.CaravanPositions);
            var c3 = Enumerable.SequenceEqual(origin.Relations, loaded.Relations);
            var c4 = Enumerable.SequenceEqual(origin.Triggers, loaded.Triggers);
            var c5 = Enumerable.SequenceEqual(origin.HappenReplacements, loaded.HappenReplacements);
            var c6 = Enumerable.SequenceEqual(origin.CampQuestTrigges, loaded.CampQuestTrigges);
            var c8 = Enumerable.SequenceEqual(origin.Parameters, loaded.Parameters);
            var c9 = Enumerable.SequenceEqual(origin.ParamTimers, loaded.ParamTimers);
            var c10 = Enumerable.SequenceEqual(origin.ActivedHappenings, loaded.ActivedHappenings);
            //var c11 = origin.DialogFront == loaded.DialogFront;
            //var c12 = origin.DialogBack == loaded.DialogBack;
            var c13 = origin.CampImagePrefab == loaded.CampImagePrefab;
            var c14 = origin.CampDialogAvailbale == loaded.CampDialogAvailbale;
            var c15 = origin.Scene == loaded.Scene;
            var c16 = origin.TravelScene == loaded.TravelScene;
            var c17 = origin.Time == loaded.Time;
            var c18 = origin.PlotStep == loaded.PlotStep;
            var c19 = origin.TutorialStep == loaded.TutorialStep;

            return c1 && c2 && c3 && c4 && c5 && c6 && c8 && c9 && c10 && /*c11 && c12 &&*/ c13 && c14 && c15 && c16 && c17 && c18 && c19;
        }
    }
}
