using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Simemes.Treasures;

namespace Simemes.Inventory
{
    public class ItemMgr : MonoSingleton<ItemMgr>
    {
        public static Inventory Inventory => GameManager.instance?.SaveData?.Inventory;

        public delegate void ItemEventHandler(TreasureConfig config);

        private readonly Dictionary<int, ItemEventHandler> _handlers = new Dictionary<int, ItemEventHandler>();

        public void RegisterHandler(int type, ItemEventHandler handler)
        {
            _handlers.TryGetValue(type, out var events);
            if (events == null)
            {
                events = handler;
                _handlers[type] = handler;
            }
            else
                events += handler;
        }

        public void RemoveHandler(int type, ItemEventHandler handler)
        {
            _handlers.TryGetValue(type, out var events);
            if (events != null)
                events -= handler;
        }

        public bool UseItem(TreasureConfig item)
        {
            bool success = Inventory.RemoveItem(item.ID, 1);

            if (!success)
                return false;

            _handlers.TryGetValue(item.Type, out var events);
            if (events != null)
                events.Invoke(item);

            if (success)
                GameManager.instance.SavePlayerData();

            return true;

        }

        public void AddItem(TreasureConfig item, int count)
        {
            AddItem(item.ID, count);
        }

        public void AddItem(int id, int count)
        {
            bool success = Inventory.AddItem(id, count);

            if (success)
                GameManager.instance.SavePlayerData();
        }
    } 
}
