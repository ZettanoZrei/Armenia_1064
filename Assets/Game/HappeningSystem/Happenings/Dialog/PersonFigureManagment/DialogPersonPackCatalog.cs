using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.HappeningSystem
{
    [CreateAssetMenu(fileName ="DialogPersonCatalog", menuName = "View/DialogPersonCatalog")]
    public class DialogPersonPackCatalog : ScriptableObject, IEnumerable<DialogPersonPack>
    {
        [SerializeField]
        private List<DialogPersonPack> dialogPersonPacks = new List<DialogPersonPack>();

        public DialogPersonPack GetPack(string name)
        {
            try
            {
                return dialogPersonPacks.First(x => x.Name == name);
            }
            catch
            {
                throw new System.Exception($"there isn't name: {name}");
            }
        }
        public IEnumerator<DialogPersonPack> GetEnumerator()
        {
            foreach(DialogPersonPack pack in dialogPersonPacks)
                yield return pack;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
}
