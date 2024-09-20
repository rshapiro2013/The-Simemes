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

        [SerializeField]
        private UIItemInfo _itemInfoPanel;

        public void PurchaseItem(UIShopItemSlot itemSlot)
        {
            bool success = ShopMgr.instance.Purchase(itemSlot.Data);

            if (success)
                EnablePanel(false);
        }

        public void ShowItemInfo(ShopItemConfig config)
        {
            _itemInfoPanel.Set(config);
            _itemInfoPanel.EnablePanel(true);
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            foreach (var slot in _slots)
                slot.Init();
        }
    }
}