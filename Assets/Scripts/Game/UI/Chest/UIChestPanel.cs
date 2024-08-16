using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core.UI;
using Simemes.Treasures;
using Simemes.AirDrop;

namespace Simemes.UI
{
    public class UIChestPanel : UIPanel
    {
        [SerializeField]
        private List<UIChestSlot> _slots;

        [Header("Debug")]
        [SerializeField]
        private List<ChestData> _fakeChestData;

        [Header("Events")]
        [SerializeField]
        private UnityEvent _onViewChestList;

        private UIChestSlot _selectedSlot;

        protected override void Awake()
        {
            base.Awake();

            RefreshSlots();
        }

        public void Click(UIChestSlot slot)
        {
            // 格子上鎖
            if (slot.Locked)
                return;

            _selectedSlot = slot;

            // 沒有箱子
            if (slot.Content == null)
                _onViewChestList.Invoke();
            // 箱子是空的
            else if (slot.Content.IsEmpty)
                AddTreasureIntoChest(slot);
            // 箱子有裝東西
            else
                ShowChestInfo(slot);
        }

        public void AddChest()
        {
            AddChest(0);
        }

        public void AddChest(int treasureBoxID)
        {
            var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(treasureBoxID);
            if (treasureBoxConfig == null)
                return;

            if (_selectedSlot != null)
                _selectedSlot.SetBox(treasureBoxConfig);
        }

        private void RefreshSlots()
        {
            for(int i=0;i<_slots.Count;++i)
            {
                if (i < _fakeChestData.Count)
                    _slots[i].Init(_fakeChestData[i]);
            }
        }

        private void AddTreasureIntoChest(UIChestSlot slot)
        {
            var item = AirDropSystem.instance.GetFirstItem();
            slot.SetTreasure(item);
        }

        private void ShowChestInfo(UIChestSlot slot)
        {

        }
    }
}