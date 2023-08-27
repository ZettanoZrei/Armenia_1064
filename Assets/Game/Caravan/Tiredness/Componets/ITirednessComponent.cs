using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


interface ITirednessComponent
{
    event Action<float> OnDecreaseStamina;
    void DoTired();
    void StopTired();

}

