using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.Game.Plot.Scripts
{
    class PlotHelper
    {
        public IEnumerable<XElement> Deserialize(TextAsset textAsset)
        {
            var xDoc = XDocument.Parse(textAsset.text);
            return xDoc.Root.Element("texts").Elements();
        }

        public string DeleteEmptySpaces(string text)
        {
            string result = text;
            //string pattern = @"(  +)|(\n\t\t)";
            string pattern = @"(\t)";
            Regex regex = new Regex(pattern);
            while (true)
            {
                var match = regex.Match(result);
                if (!match.Success)
                    break;
                result = result.Replace(match.Value, "");
            }
            return result;
        }
    }
}
