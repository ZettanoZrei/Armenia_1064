using Assets.Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public static class StringExtensions
    {
        public static Tag ToTag(this string stringTag) 
        {
            foreach(Tag tag in Enum.GetValues(typeof(Tag)))
            {
                if (tag.ToString() == stringTag)
                    return tag;
            }

            throw new Exception($"Wrong tag {stringTag}!");
        }

    }
}
