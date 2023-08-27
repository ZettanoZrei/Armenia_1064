using Model.Entities;
using System;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class SaveConfing : IClone<SaveConfing>
    {
        public bool isSave;
        public float timeSave;

        public SaveConfing Clone()
        {
            return new SaveConfing
            {
                isSave = isSave,
                timeSave = timeSave
            };
        }
    }
}
