using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core.UI;

using Simemes.Shop;

namespace Simemes.UI
{
    public class UIStorePanel : UIPanel
    {
        [SerializeField]
        private List<UIShopItemSlot> _slots;

        public void PurchaseItem(UIShopItemSlot itemSlot)
        {
            bool success = ShopMgr.instance.Purchase(itemSlot.Data);

            if (success)
                EnablePanel(false);
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            foreach (var slot in _slots)
                slot.Init();
        }
    }
}