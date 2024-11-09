using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Inventory
{
    public class Inventory
    {
        public Dictionary<int, int> Items = new Dictionary<int, int>();

        private readonly List<KeyValuePair<int, int>> _filteredItems = new List<KeyValuePair<int, int>>();

        public int GetItemCount(int id)
        {
            if (Items == null)
                return 0;

            Items.TryGetValue(id, out int count);
            return count;
        }

        public bool CheckCount(int id, int count)
        {
            return GetItemCount(id) >= count;
        }

        public bool AddItem(int id, int add)
        {
            if (Items == null)
                Items = new Dictionary<int, int>();

            Items.TryGetValue(id, out int count);
            count += add;

            Items[id] = count;

            return true;
        }

        public bool RemoveItem(int id, int used)
        {
            if (Items == null)
                return false;

            Items.TryGetValue(id, out int count);
            if (count < used)
                return false;

            count -= used;

            if (count <= 0)
                Items.Remove(id);
            else
                Items[id] = count;

            return true;
        }

        /// <summary>
        /// Get item list for specific type
        /// </summary>
        /// <param name="type">Use ItemType.xxxx to represent type</param>
        /// <returns></returns>
        public List<KeyValuePair<int,int>> GetItems(int type)
        {
            _filteredItems.Clear();

            if (Items != null)
            {
                foreach (var kvp in Items)
                {
                    if (CheckItemType(kvp.Key, type))
                        _filteredItems.Add(kvp);
                }
            }

            return _filteredItems;
        }

        public bool CheckItemType(int id, int type)
        {
            return id >= type && id < type + 100;
        }
    }
}
