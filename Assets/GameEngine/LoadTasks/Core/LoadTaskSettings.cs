using Assets.Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameEngine.LoadTasks
{
    internal class LoadTaskSettings : IStepConfig
    {
        public bool Activate { get; set; } = true;

        public int StartStep { get; set; }
    }

}
