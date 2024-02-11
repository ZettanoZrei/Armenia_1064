using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using System.Reflection;
using ModestTree;
using Assets.Game.HappeningSystem.ManagementHappens;
using Assets.Game.HappeningSystem;

namespace ExtraInjection
{
    //Этот класс служит для того чтобы внедрять зависимости которых нет в глобальном контексете в глобальные классы,
    //когда они переходят на новые сцены в контексте которых они есть
    public class ExtraInjector : IInitializable
    {
        private readonly DiContainer container;

        public ExtraInjector(DiContainer container)
        {
            this.container = container;
        }

        void IInitializable.Initialize()
        {
            var all = ProjectContext.Instance.Container.ResolveAll<IExtraInject>();
            foreach (var glob in all)
            {
                var fields = glob.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var field in fields)
                {
                    var attrs = field.GetCustomAttributes(false);
                    foreach (var attr in attrs)
                    {
                        if (attr.GetType() == typeof(ExtraInjectAttribute))
                        {
                            try
                            {
                                var value = container.Resolve(field.FieldType);
                                field.SetValue(glob, value);
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }


        }


    }
}
