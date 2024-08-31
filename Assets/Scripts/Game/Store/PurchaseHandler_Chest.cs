using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    public class PurchaseHandler_Chest : PurchaseHandler
    {
        [SerializeField]
        protected Simemes.UI.UIChestPanel _chestPanel;

        public override bool HandlePurchase(ShopItemConfig item)
        {
            base.HandlePurchase(item);

            var chest = item as ShopItem_Chest;

            bool success =_chestPanel.AddChest(chest.ChestID);

            if (success)
                OnPurchaseSuccess();

            return success;
        }
    }
}
