using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField] GameObject pref;
    public Transform container;

    [ContextMenu("Do")]
    public void Do()
    {
        Instantiate(pref, container);
    }

}

class Foo
{
    public string name;
}


