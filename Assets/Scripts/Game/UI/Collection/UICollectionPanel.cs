using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Simemes.Treasures;

namespace Simemes.UI
{
    public class UICollectionPanel : UIPanel
    {
        [SerializeField]
        private UICollectionItemSlot _itemInfo;

        [SerializeField]
        private UIPanel _itemInfoPanel;

        [SerializeField]
        private UIElementList _collectionList;

        protected override void OnShowPanel()
        {
            base.OnShowPanel();
            Init();
        }

        public void Init()
        {
            var treasures = TreasureSystem.instance.TreasureItems;
            var collectionSaveData = GameManager.instance.SaveData.Collection;

            _collectionList.Clear();

            for(int i=0;i<treasures.Count;++i)
            {
                var element = _collectionList.CreateElement<UICollectionItemSlot>();
                bool isUnlocked = collectionSaveData.IsUnlocked(treasures[i].ID);
                element.Set(treasures[i], isUnlocked);
                element.gameObject.SetActive(true);
            }
        }

        public void ShowDetailInfo(TreasureConfig treasure)
        {
            _itemInfo.Set(treasure, true);
            _itemInfoPanel.EnablePanel(true);
        }
    }
}
