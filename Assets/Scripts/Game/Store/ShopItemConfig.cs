using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    public enum ShopItemType
    {
        Chest,
        Buff,
        Reward
    }

    public class ShopItemConfig : ScriptableObject
    {
        [SerializeField]
        protected string _name;

        [SerializeField]
        protected ShopItemType _itemType;

        [SerializeField]
        protected int _itemCount;

        [SerializeField]
        protected Sprite _icon;

        [SerializeField]
        protected int _currencyType;

        [SerializeField]
        protected int _price;

        [SerializeField]
        protected float _usd;

        [TextArea(3,10)]
        [SerializeField]
        protected string _desc;

        public string Name => _name;
        public ShopItemType ItemType => _itemType;

        public Sprite Icon => _icon;
        public int CurrencyType => _currencyType;
        public int Price => _price;
        public int ItemCount => _itemCount;

        public float USD => _usd;

        public string Desc => _desc;
    }
}
