using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Plot
{
    internal class Debug_Plot : MonoBehaviour
    {
        private void Start()
        {
            var res = Mathf.Lerp(1, 3, 0.1f);
        }
    }
}
