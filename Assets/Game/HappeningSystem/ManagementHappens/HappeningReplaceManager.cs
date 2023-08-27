using Model.Entities.Answers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Game.HappeningSystem
{
    public class HappeningReplaceManager : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly Dictionary<string, string> replaceInfo = new Dictionary<string, string>();

        public void AddHappeningRaplacement(SingleHappeningConsequences consequences)
        {
            if (replaceInfo.ContainsKey(consequences.OldHappening))
                throw new Exception("ReplaceHappeningManager error");

            replaceInfo.Add(consequences.OldHappening, consequences.NewHappening);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, string>>)replaceInfo).GetEnumerator();
        }

        public void Clear()
        {
            replaceInfo.Clear();
        }

        public bool TryGetReplacement(string happening, out string replacement)
        {
            return replaceInfo.TryGetValue(happening, out replacement);            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)replaceInfo).GetEnumerator();
        }


    }
}
