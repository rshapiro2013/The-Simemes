using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.UI;
using Simemes.Inventory;
using Simemes.Treasures;

namespace Simemes.UI
{
    public class UIInventoryPanel : UIPanel
    {
        [SerializeField]
        private UIElementList _slots;

        [SerializeField]
        private UIItemInfo _itemInfoPanel;

        public void UpdateList(int itemType)
        {
            _slots.Clear();

            var list = ItemMgr.Inventory.GetItems(itemType);

            foreach(var kvp in list)
            {
                var item = TreasureSystem.instance.GetTreasureConfig(kvp.Key);
                if (item == null)
                    continue;

                var element = _slots.CreateElement<UIItemSlot>();
                element.Set(item);
                element.gameObject.SetActive(true);
            }
        }

        public void ShowItemInfo(TreasureConfig config)
        {
            _itemInfoPanel.Set(config);
            _itemInfoPanel.EnablePanel(true);
        }

        public void ShowItems(int itemType)
        {
            UpdateList(itemType);
            EnablePanel(true);
        }

        public void ClickItem(UIItemSlot slot)
        {
            bool success = ItemMgr.instance.UseItem(slot.Data);
            if (success)
                EnablePanel(false);
        }
    }
}
