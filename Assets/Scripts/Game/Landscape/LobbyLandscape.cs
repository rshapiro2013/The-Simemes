using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Core.Utilities;

namespace Simemes.Landscape
{
    // Lobby場景物件管理
    public class LobbyLandscape : MonoSingleton<LobbyLandscape>
    {
        [SerializeField]
        protected RectTransform _itemRoot;

        protected readonly Dictionary<ILandscapeItem, GameObject> _items = new Dictionary<ILandscapeItem, GameObject>();

        /// <summary>
        /// 新增物件到場上
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ILandscapeItem item)
        {
            // prefab可能有不同功能行為
            var instance = Poolable.TryGetPoolable(item.Prefab);

            if(!_items.ContainsKey(item))
            {
                _items[item] = instance;
            }
            else
            {
                Debug.LogError("物件已經存在!");
                return;
            }

            // 如果需要換圖的話
            var image = instance.GetComponent<Image>();
            if (image != null && item.Image != null)
            {
                image.sprite = item.Image;
                image.SetNativeSize();
            }

            // 隨機決定位置
            instance.transform.SetParent(_itemRoot, false);
            var min = _itemRoot.rect.min;
            var max = _itemRoot.rect.max;

            (instance.transform as RectTransform).anchoredPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }

        /// <summary>
        /// 表演將物件撿起到目的地
        /// </summary>
        /// <param name="item"></param>
        /// <param name="destination"></param>
        /// <param name="onReach"></param>
        public void PickItem(ILandscapeItem item, Vector3 destination, System.Action<ILandscapeItem> onReach = null)
        {
            if (!_items.TryGetValue(item, out var instance))
                return;

            _items.Remove(item);
            Poolable.TryPool(instance);
            onReach?.Invoke(item);
        }
    }
}