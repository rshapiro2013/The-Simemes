using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Inventory;

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

            //_chestPanel.Enchant(buff.BuffID, OnPurchaseSuccess);

            // 改成先加入背包
            ItemMgr.instance.AddItem(buff.BuffID, 1);
            return true;
        }
    }
}
