using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.Game.Plot.Scripts
{
    [CreateAssetMenu(
        fileName = "PlotDialogModel",
        menuName = "GameEngine/PlotDialogModel"
    )]
    internal class PlotDialogModel : ScriptableObject
    {
        [SerializeField] private TextAsset textDialogFile;
        [NonSerialized] private PlotHelper plotHelper = new PlotHelper();
        [NonSerialized] private List<PhrasesInfo> phrases;

        [NonSerialized] private int count;
        public bool TryGetPhrase(out PhrasesInfo phrase)
        {
            if (phrases.Count > count)
            {
                phrase = phrases[count];
                count++;
                return true;
            }
            phrase = default;
            return false;
        }

        public void ResetCount()
        {
            count = 0;
        }

        private void OnEnable()
        {
            if (textDialogFile == null)
                return;
            var xElems = plotHelper.Deserialize(textDialogFile);
            phrases = GetPhrasesInfos(xElems);
        }
        private List<PhrasesInfo> GetPhrasesInfos(IEnumerable<XElement> xElements)
        {
            return xElements
                .Select(x => new PhrasesInfo
                {
                    Person = x.Attribute("person").Value,
                    Phrase = plotHelper.DeleteEmptySpaces(x.Value),
                    Time = int.Parse(x.Attribute("time").Value)
                })
                .ToList();
        }
    }

    struct PhrasesInfo
    {
        public string Phrase { get; set; }
        public string Person { get; set; }
        public int Time { get; set; }
    }
}
