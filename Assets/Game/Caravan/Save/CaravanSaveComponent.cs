using Assets.Game.Caravan.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class CaravanSaveComponent : MonoBehaviour, ISaveComponent<List<CaravanData>>
{
    [SerializeField]
    private SaveMechanics saveMechanics;
    public List<CaravanData> GetData()
    {
        return saveMechanics.GetData();
    }

    public void LoadData(List<CaravanData> caravanData)
    {
        saveMechanics.LoadData(caravanData);
    }
}

