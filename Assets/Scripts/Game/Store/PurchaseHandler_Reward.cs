using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    public class PurchaseHandler_Reward : PurchaseHandler
    {
        public override bool HandlePurchase(ShopItemConfig item)
        {
            base.HandlePurchase(item);

            var reward = item as ShopItem_Reward;

            Rewards.RewardMgr.instance.ObtainReward(reward.RewardID, reward.ItemCount);

            return true;
        }
    }
}
