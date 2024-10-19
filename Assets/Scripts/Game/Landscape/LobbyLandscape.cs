using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using Core.Utilities;

namespace Simemes.Landscape
{
    // Lobby場景物件管理
    public class LobbyLandscape : MonoSingleton<LobbyLandscape>
    {
        [SerializeField]
        protected RectTransform _itemRoot;

        [SerializeField]
        protected DropArea _dropArea;

        [SerializeField]
        protected RectTransform _movingParent;

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
            _dropArea.GetParentAndPos(out var parent, out var pos);

            instance.transform.SetParent(parent, false);
            instance.transform.position = pos;
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

            instance.transform.SetParent(_movingParent, true);

            instance.transform.DOMove(destination, 0.5f).OnComplete(() =>
            {
                _items.Remove(item);
                Poolable.TryPool(instance);
                onReach?.Invoke(item);
            });
        }
    }
}