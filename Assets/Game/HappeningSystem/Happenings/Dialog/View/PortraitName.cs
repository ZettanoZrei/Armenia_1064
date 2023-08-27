using System.Linq;
using UnityEngine;
using UnityEngine.UI;

class PortraitName : MonoBehaviour
{
    [SerializeField] private Text text;

    public void SetPortraitName(string name)
    {
        text.text = name;
    }
}


