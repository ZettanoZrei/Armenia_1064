using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Modules.UI
{
    public class SimpleButton : MonoBehaviour
    {
        [SerializeField] private GameObject container; //or this object

        public event Action OnClick;
        public UnityEvent<bool> OnActiveButton;
        public void SetActiveObject(bool value)
        {
            container.SetActive(value);
        }

        public void SetActiveButton(bool value)
        {
            OnActiveButton?.Invoke(value);
            if (gameObject.GetComponent<Button>())
                gameObject.GetComponent<Button>().interactable = value;
        }

        //ui this button
        public void InvokeClickAction()
        {
            OnClick?.Invoke();
        }
    }
}
