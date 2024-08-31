using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes.Shop
{
    public class ShopMgr : MonoSingleton<ShopMgr>
    {

        protected Dictionary<ShopItemType, PurchaseHandler> _handlers = new Dictionary<ShopItemType, PurchaseHandler>();

        public void RegisterHandler(ShopItemType type, PurchaseHandler handler)
        {
            _handlers[type] = handler;
        }

        public void UnregisterHandler(ShopItemType type, PurchaseHandler handler)
        {
            _handlers.TryGetValue(type, out var currentHandler);
            if (currentHandler == handler)
                _handlers.Remove(type);
        }

        public bool Purchase(ShopItemConfig item)
        {
            // 沒有足夠錢
            if (!GameManager.instance.PlayerProfile.CheckCoin(item.Price))
                return false;

            // 沒有能處理該商品類型的handler
            if (!_handlers.TryGetValue(item.ItemType, out var handler))
                return false;

            // 看handler是否成功處理購買
            return handler.HandlePurchase(item);
        }

        public void OnPurchaseSuccess(ShopItemConfig item)
        {
            // 確定成功購買使用，開始扣錢
            GameManager.instance.PlayerProfile.AddCoin(-item.Price);
        }
    }
}