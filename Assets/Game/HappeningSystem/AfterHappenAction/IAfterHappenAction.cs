using Assets.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.HappeningSystem
{
    internal interface IAfterHappenAction : ICloneable<IAfterHappenAction> //todo: iclone remove? 
    {
        void Do(ICallBack callBack);
    }
}
