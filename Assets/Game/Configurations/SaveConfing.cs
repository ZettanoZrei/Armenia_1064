using Model.Entities;
using System;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class SaveConfing : IClone<SaveConfing> //TODO не нужен realtime config в данный момент
    {
        public bool isSave;
        public float timeSave;
        public bool IsPlotSegment { get; set; }
        public int savesNumber;
        public SaveConfing Clone()
        {
            return new SaveConfing
            {
                isSave = isSave,
                timeSave = timeSave,
                savesNumber = savesNumber,
                IsPlotSegment = IsPlotSegment
            };
        }
    }
}
