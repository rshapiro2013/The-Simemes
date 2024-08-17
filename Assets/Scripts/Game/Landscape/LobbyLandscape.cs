using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Core.Utilities;

namespace Simemes.Landscape
{
    // Lobby��������޲z
    public class LobbyLandscape : MonoSingleton<LobbyLandscape>
    {
        [SerializeField]
        protected RectTransform _itemRoot;

        protected readonly Dictionary<ILandscapeItem, GameObject> _items = new Dictionary<ILandscapeItem, GameObject>();

        /// <summary>
        /// �s�W�������W
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ILandscapeItem item)
        {
            // prefab�i�঳���P�\��欰
            var instance = Poolable.TryGetPoolable(item.Prefab);

            if(!_items.ContainsKey(item))
            {
                _items[item] = instance;
            }
            else
            {
                Debug.LogError("����w�g�s�b!");
                return;
            }

            // �p�G�ݭn���Ϫ���
            var image = instance.GetComponent<Image>();
            if (image != null && item.Image != null)
            {
                image.sprite = item.Image;
                image.SetNativeSize();
            }

            // �H���M�w��m
            instance.transform.SetParent(_itemRoot, false);
            var min = _itemRoot.rect.min;
            var max = _itemRoot.rect.max;

            (instance.transform as RectTransform).anchoredPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }

        /// <summary>
        /// ��t�N����߰_��ت��a
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