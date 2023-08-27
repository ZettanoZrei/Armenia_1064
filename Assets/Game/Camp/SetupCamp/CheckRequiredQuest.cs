using Assets.Modules;
using UnityEngine;

namespace Assets.Game.Camp
{
    public class CheckRequiredQuest
    {
        public bool Execute()
        {
            var openIcons = MonoBehaviour.FindObjectsOfType<CampIcon>();
            foreach(var icon in openIcons)
            {
                if (icon.IsRequired)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
