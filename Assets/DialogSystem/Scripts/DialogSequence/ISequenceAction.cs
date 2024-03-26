using Assets.Modules;
using System.Threading.Tasks;

namespace Assets.DialogSystem.Scripts
{
    public interface ISequenceAction
    {
        Task Execute(ICallBack callBack);
    }
}
