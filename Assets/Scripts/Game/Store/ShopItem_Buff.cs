using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    [CreateAssetMenu(fileName = "ShopItem Buff", menuName = "Simemes/ShopItem/Buff")]
    public class ShopItem_Buff : ShopItemConfig
    {
        [SerializeField]
        protected int _buffID;

        public int BuffID => _buffID;

    }
}
