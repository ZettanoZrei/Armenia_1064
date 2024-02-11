using Assets.Modules;
using ExtraInjection;
using Model.Entities.Happenings;

namespace Assets.Game.HappeningSystem
{
    public interface IHappeningManager
    {
        void Perform();
        void Init(Happening data, ICallBack callBack);
    }
}
