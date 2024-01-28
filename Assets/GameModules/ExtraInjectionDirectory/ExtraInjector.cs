using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameModules.ExtraInjectionDirectory
{
    public interface IExtraInjection<T>
    {
        void Inject(T dependency);
    }
    internal class ExtraInjector
    {
    }
}
