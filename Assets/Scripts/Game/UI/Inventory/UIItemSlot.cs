using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Simemes.Treasures;
using Simemes.Inventory;
using Core.UI;

namespace Simemes.UI
{
    public class UIItemSlot : UIElement
    {
        [SerializeField]
        protected UIInventoryPanel _parentPanel;

        [SerializeField]
        protected TextMeshProUGUI _name;

        [SerializeField]
        protected TextMeshProUGUI _count;

        [SerializeField]
        protected Image _icon;

        [SerializeField]
        protected Button _button;

        [SerializeField]
        protected Color _color_Normal;

        [SerializeField]
        protected Color _color_Invalid;

        protected TreasureConfig _data;

        public TreasureConfig Data => _data;

        public void Set(TreasureConfig item)
        {
            _data = item;

            _name.text = item.Name;

            _icon.sprite = item.Image;
            _icon.SetNativeSize();

            var inventory = ItemMgr.Inventory;
            int count = inventory.GetItemCount(item.ID);
            bool canUse = inventory.CheckCount(item.ID, 1);

            _count.text = $"Amount: {count}";

            _button.interactable = canUse;
        }

        // 顯示商品資訊
        public void ShowItemInfo()
        {
            _parentPanel.ShowItemInfo(_data);
        }

        public void Click()
        {
            _parentPanel.ClickItem(this);
        }
    }
}
