using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Cinemachine;
using Assets.Game.Plot.Scripts;

namespace Assets.Game.Plot
{
    [CreateAssetMenu(
        fileName = "PlotStoryModel",
        menuName = "GameEngine/PlotStoryModel"
    )]

    internal class PlotStoryModel : ScriptableObject
    {
        [SerializeField] private TextAsset textMapFile;
        [SerializeField] private GameObject picturePrefab;

        [NonSerialized] private List<string> texts = new List<string>();
        [NonSerialized] private int textIndex = 0;

        public GameObject PicturePrefab => picturePrefab;
        private PlotHelper plotHelper = new PlotHelper();

        public bool TryGetText(out string text)
        {
            if (textIndex < texts.Count)
            {
                text = texts[textIndex];
                textIndex++;
                return true;
            }
            text = null;
            return false;
        }


        private void OnEnable()
        {
            Debug.Log("PlotStoryModel -OnEnable");
            var xTexts = plotHelper.Deserialize(textMapFile);
            foreach (var xText in xTexts)
            {
                var text = plotHelper.DeleteEmptySpaces(xText.Value);
                texts.Add(text);
            }
        }
    }
}
