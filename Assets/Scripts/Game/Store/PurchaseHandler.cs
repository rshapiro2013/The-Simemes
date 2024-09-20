using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Shop
{
    public abstract class PurchaseHandler : MonoBehaviour
    {
        [SerializeField]
        protected ShopItemType _targetItemType;

        protected ShopItemConfig _item;

        protected void Awake()
        {
            RegisterHandler();
        }

        protected void OnDestroy()
        {
            UnregisterHandler();
        }

        protected virtual void RegisterHandler()
        {
            ShopMgr.instance.RegisterHandler(_targetItemType, this);
        }

        protected virtual void UnregisterHandler()
        {
            ShopMgr.instance?.UnregisterHandler(_targetItemType, this);
        }

        public virtual bool HandlePurchase(ShopItemConfig item)
        {
            _item = item;
            return false;
        }

        protected void OnPurchaseSuccess()
        {
            ShopMgr.instance.OnPurchaseSuccess(_item);
        }
    }
}
