using Assets.Game.Camp.Background;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Game.Camp.IconsSystem
{
    public class IconsFieldAllocator 
    {
        public void AllocateIcons(List<CampIcon> portraits, RectTransform[] fields)
        {
            var parentSize = fields[0].sizeDelta;
            for (var i = 0; i < portraits.Count; i++)
            {
                var icon = portraits[i];
                var xLimit = parentSize.x / 2 - icon.GetSize().x / 2;
                var yLimit = parentSize.y / 2 - icon.GetSize().y / 2;
                var position = new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), 0);
                icon.transform.parent = fields[i].transform;
                icon.SetTransformSettings(position);
            }
        }
    }
}
