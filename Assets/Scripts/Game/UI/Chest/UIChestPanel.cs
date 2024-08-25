using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        [SerializeField]
        private Button _button_DisableEnchant;

        [Header("Debug")]
        [SerializeField]
        private List<ChestData> _fakeChestData;

        [Header("Events")]
        [SerializeField]
        private UnityEvent _onViewChestList;

        private UIChestSlot _selectedSlot;

        private int _buffIdx;
        private bool _enchantMode;

        protected override void Awake()
        {
            base.Awake();

            RefreshSlots();

            DisableEnchantMode();
        }

        public void Click(UIChestSlot slot)
        {
            // 格子上鎖
            if (slot.Locked)
                return;

            // 附魔模式
            if(_enchantMode)
            {
                Enchant(slot);
                return;
            }

            _selectedSlot = slot;

            // 沒有箱子
            if (slot.Content == null)
                _onViewChestList.Invoke();
            // 箱子沒有滿
            else if (!slot.Content.IsFull)
                AddTreasureIntoChest(slot);
            // 箱子滿了
            else
                ShowChestInfo(slot);
        }

        public void Hold(UIChestSlot slot)
        {
            // 箱子不是空的就可以關上倒數
            if (slot.Content != null)
                slot.Seal();
        }

        public void Enchant(UIChestSlot slot)
        {
            if (slot.Content == null || !slot.Content.IsSealed || slot.Content.HasBuff)
                return;

            slot.AddBuff(TreasureSystem.instance.GetBuff(_buffIdx));

            DisableEnchantMode();
        }

        // 開啟附魔模式，要設定附魔的buffIdx
        public void EnableEnchantMode(int buffIdx)
        {
            _enchantMode = true;
            _buffIdx = buffIdx;

            _button_DisableEnchant.gameObject.SetActive(true);
        }

        public void DisableEnchantMode()
        {
            _enchantMode = false;

            _button_DisableEnchant.gameObject.SetActive(false);
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