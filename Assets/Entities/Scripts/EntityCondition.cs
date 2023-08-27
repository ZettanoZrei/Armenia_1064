using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Entities
{
    public abstract class EntityCondition : ScriptableObject
    {
        public abstract bool IsTrue(IEntity entity);
    }
}
