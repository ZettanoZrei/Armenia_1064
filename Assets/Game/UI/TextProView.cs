using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TMPro;
using UnityEngine;

namespace Assets.Game.UI
{
    internal class TextProView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private bool isAlign;

        public void AssignString(string text)
        {
            tmpText.text = text;

            //if(isAlign)
            //    StartCoroutine(DelayAlign());
        }

        private void OnRenderObject()
        {
            //if (isAlign)
            //    Align();
        }

        

        [ContextMenu("AssignString")]
        void Align()
        {
            // Updates the Text-Mesh
            tmpText.ForceMeshUpdate();

            // Get TextBounds and use TextBounds to position the Text-Parent
            Bounds bounds = tmpText.textBounds;
            Vector2 newPos = new Vector2(-bounds.center.x, -bounds.center.y);

            RectTransform rt = tmpText.transform.parent.GetComponent<RectTransform>();
            rt.localPosition = newPos;
        }
    }
}
