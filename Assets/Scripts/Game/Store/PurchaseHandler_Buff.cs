using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    public class PurchaseHandler_Buff : PurchaseHandler
    {
        [SerializeField]
        protected Simemes.UI.UIChestPanel _chestPanel;

        public override bool HandlePurchase(ShopItemConfig item)
        {
            base.HandlePurchase(item);

            var buff = item as ShopItem_Buff;

            _chestPanel.EnableEnchantMode(buff.BuffID, OnPurchaseSuccess);
            return true;
        }
    }
}
