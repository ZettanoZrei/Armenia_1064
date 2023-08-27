using Assets.Game.HappeningSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Camp
{
    public class CampIncomingData
    {
        public event Action<int> OnDialogAvailableChange;
        public int DialogAvailable { get; private set; }
        public string CampImagePrefab { get; set; }
        public List<Quest> CampQuests { get; set; } = new List<Quest>();

        public void SetDialogAvailable(int value)
        {
            DialogAvailable = value;
        }

        public void MinusDialogAvailable(int value)
        {
            DialogAvailable -= value;
            OnDialogAvailableChange?.Invoke(DialogAvailable);
        }

        public void MinusCampDialog(string questTitle)
        {
            var quest = CampQuests.First(x => x.Title == questTitle);
            CampQuests.Remove(quest);
        }
    }
}
