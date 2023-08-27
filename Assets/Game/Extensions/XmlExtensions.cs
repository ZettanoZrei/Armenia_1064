using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public static class XmlExtensions
    {
        public static bool TryGetElement(this XElement element, string elementName, out XElement output)
        {
            foreach (var elem in element.Elements())
            {
                if (elem.Name == elementName)
                {
                    output = elem;
                    return true;
                }
            }
            output = null;
            return false;
        }
        public static bool HasElement(this XElement element, string elementName)
        {
            foreach(var elem in element.Elements())
            {
                if (elem.Name == elementName)
                    return true;
            }
            return false;
        }

        public static bool HasAttribute(this XElement element, string attributeName)
        {
            foreach (var attr in element.Attributes())
            {
                if (attr.Name == attributeName)
                    return true;
            }
            return false;
        }
    }
}
