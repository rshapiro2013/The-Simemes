using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    [CreateAssetMenu(fileName = "ShopItem Reward", menuName = "Simemes/ShopItem/Reward")]
    public class ShopItem_Reward : ShopItemConfig
    {
        [SerializeField]
        protected int _rewardID;

        public int RewardID => _rewardID;

    }
}
