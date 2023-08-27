using Model.Entities.Persons;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.HappeningSystem
{
    public class PersonModelInitComponent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Person> OnInitInfo;

        [SerializeField]
        private UnityEvent<Sprite, Sprite, int> OnInitPack;
        public void Init(Person personInfo, DialogPersonPack dialogPersonPack)
        {
            OnInitInfo?.Invoke(personInfo);
            OnInitPack?.Invoke(dialogPersonPack.Front, dialogPersonPack.Back, personInfo.Layer);
        }
    }
}
