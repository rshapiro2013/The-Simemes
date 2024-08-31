using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    [CreateAssetMenu(fileName = "ShopItem Chest", menuName = "Simemes/ShopItem/Chest")]
    public class ShopItem_Chest : ShopItemConfig
    {
        [SerializeField]
        protected int _chestID;

        public int ChestID => _chestID;
    }
}
