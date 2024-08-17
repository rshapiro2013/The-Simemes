using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core.UI;
using Simemes.Treasures;
using Simemes.AirDrop;
using Simemes.Landscape;

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
            // ��l�W��
            if (slot.Locked)
                return;

            _selectedSlot = slot;

            // �S���c�l
            if (slot.Content == null)
                _onViewChestList.Invoke();
            // �c�l�O�Ū�
            else if (slot.Content.IsEmpty)
                AddTreasureIntoChest(slot);
            // �c�l���˪F��
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
            {
                var treasureBox = new TreasureBox(treasureBoxConfig);
                _selectedSlot.SetBox(treasureBox);
            }
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

            if (item == null)
                return;

            AirDropSystem.instance.RemoveFirstItem();
            LobbyLandscape.instance.PickItem(item, slot.transform.position, (treasure) => slot.SetTreasure(treasure as ITreasure));
        }

        private void ShowChestInfo(UIChestSlot slot)
        {

        }
    }
}