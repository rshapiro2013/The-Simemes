using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Simemes.Treasures;

namespace Simemes.UI
{
    public class UICollectionItemSlot : Core.UI.UIElement
    {
        [SerializeField]
        protected Image _icon;

        [SerializeField]
        protected TextMeshProUGUI _name;

        [SerializeField]
        protected UICollectionPanel _parentPanel;

        [SerializeField]
        protected TextMeshProUGUI _effect;

        [SerializeField]
        protected TextMeshProUGUI _desc;

        [SerializeField]
        protected Sprite _lockIcon;

        protected TreasureConfig _data;

        public TreasureConfig Data => _data;

        private bool _isUnlocked;

        public void Set(TreasureConfig treasure, bool isUnlocked)
        {
            _data = treasure;
            _isUnlocked = isUnlocked;

            _icon.sprite = isUnlocked ? treasure.Image : _lockIcon;
            _name.text = isUnlocked ? treasure.Name : "Unknown";
            _effect.text = treasure.GetEffect();

            if (_desc != null)
                _desc.text = treasure.Desc;

            if (_effect != null)
                _effect.gameObject.SetActive(isUnlocked);
        }

        public void ShowDetailInfo()
        {
            if (!_isUnlocked)
                return;

            _parentPanel.ShowDetailInfo(_data);
        }
    }
}
