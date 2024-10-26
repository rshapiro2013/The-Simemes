using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Simemes.Shop;

namespace Simemes.UI
{
    public class UIShopItemSlot : MonoBehaviour
    {
        [SerializeField]
        protected ShopItemConfig _data;

        [SerializeField]
        protected UIStorePanel _storePanel;

        [SerializeField]
        protected TextMeshProUGUI _name;

        [SerializeField]
        protected Image _currency;

        [SerializeField]
        protected TextMeshProUGUI _price;

        [SerializeField]
        protected Image _icon;

        [SerializeField]
        protected Button _purchaseButton;

        [SerializeField]
        protected Color _color_Normal;

        [SerializeField]
        protected Color _color_Invalid;

        public ShopItemConfig Data => _data;

        public void Init()
        {
            if (_data != null)
                Set(_data);
        }

        public void Set(ShopItemConfig item)
        {
            _data = item;

            _name.text = item.Name;

            _icon.sprite = item.Icon;
            _icon.SetNativeSize();

            bool canBuy = ShopMgr.instance.CanBuy(item);

            _price.text = item.Price.ToString();
            _price.color = canBuy ? _color_Normal : _color_Invalid;

            if (item.CurrencyType == 1)
            {
                _currency.gameObject.SetActive(false);
                _price.text = $"{item.USD} USD";
            }
            else
            {
                _currency.gameObject.SetActive(true);

                var currencyConfig = Rewards.RewardMgr.instance.GetRewardConfig(item.CurrencyType);
                if (currencyConfig != null)
                    _currency.sprite = currencyConfig.Image;
            }
        }
        
        // 顯示商品資訊
        public void ShowItemInfo()
        {
            _storePanel.ShowItemInfo(_data);
        }

        public void Purchase()
        {
            bool canBuy = ShopMgr.instance.CanBuy(_data);

            if (canBuy)
                _storePanel.PurchaseItem(this);
        }
    }
}
