using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Shop;
using Core.UI;

namespace Simemes.UI
{
    public class UIItemInfo : UIPanel
    {
        [SerializeField]
        protected TextMeshProUGUI _name;

        [SerializeField]
        protected Image _icon;

        [SerializeField]
        protected TextMeshProUGUI _description;

        public void Set(ShopItemConfig config)
        {
            _name.text = config.Name;

            _icon.sprite = config.Icon;
            _icon.SetNativeSize();

            _description.text = config.Desc;
        }
    }
}
