using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    public enum ShopItemType
    {
        Chest,
        Buff
    }

    public class ShopItemConfig : ScriptableObject
    {
        [SerializeField]
        protected string _name;

        [SerializeField]
        protected ShopItemType _itemType;

        [SerializeField]
        protected Sprite _icon;

        [SerializeField]
        protected int _currencyType;

        [SerializeField]
        protected int _price;

        public string Name => _name;
        public ShopItemType ItemType => _itemType;

        public Sprite Icon => _icon;
        public int CurrencyType => _currencyType;
        public int Price => _price;
    }
}
