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
            // �S��������
            if (!GameManager.instance.PlayerProfile.CheckCoin(item.Price))
                return false;

            // �S����B�z�Ӱӫ~������handler
            if (!_handlers.TryGetValue(item.ItemType, out var handler))
                return false;

            // ��handler�O�_���\�B�z�ʶR
            return handler.HandlePurchase(item);
        }

        public void OnPurchaseSuccess(ShopItemConfig item)
        {
            // �T�w���\�ʶR�ϥΡA�}�l����
            GameManager.instance.PlayerProfile.AddCoin(-item.Price);
        }
    }
}