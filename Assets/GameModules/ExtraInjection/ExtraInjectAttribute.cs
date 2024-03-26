using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraInjection
{
    /// <summary>
    /// Внедряет новую зависимость в глобальный класс при каждом переходе на новую сцену
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ExtraInjectAttribute : Attribute
    {
    }

    //Указатель на класс где следует провести дополнительное внедрение зависимостей
    public interface IExtraInject { }
}
