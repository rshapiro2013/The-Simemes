using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Core.UI;
using Simemes.Treasures;
using Simemes.AirDrop;
using Simemes.Landscape;
using Simemes.Tasks;

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

        private System.Action _onEnchant;

        public List<UIChestSlot> Slots => _slots; 

        protected override void Awake()
        {
            base.Awake();

            DisableEnchantMode();

            TreasureSystem.instance.OnUpdateChests += RefreshSlots;
            GameManager.instance.PlayerProfile.OnUpdateTierData += RefreshSlotLocks;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (TreasureSystem.instanceExists)
                TreasureSystem.instance.OnUpdateChests -= RefreshSlots;
            if (GameManager.instanceExists)
                GameManager.instance.PlayerProfile.OnUpdateTierData -= RefreshSlotLocks;
        }

        public void Click(UIChestSlot slot)
        {
            // ��l�W��
            if (slot.Locked)
                return;

            // ���]�Ҧ�
            if(_enchantMode)
            {
                Enchant(slot);
                return;
            }

            _selectedSlot = slot;

            // �S���c�l
            if (slot.Content == null)
                _onViewChestList.Invoke();
            // �c�l�S�����]�٨S��
            else if (!slot.Content.IsFull && !slot.Content.IsSealed)
                AddTreasureIntoChest(slot);
            // �c�l���F
            else if(slot.Content.IsSealed)
                ShowChestInfo(slot);
        }

        public void Hold(UIChestSlot slot)
        {
            // �c�l���O�Ū��N�i�H���W�˼�
            if (slot.Content != null && !slot.Content.IsSealed)
            {
                slot.Seal();
                int slotIdx = _slots.FindIndex(x => x == slot);

                Simemes.Request.TreasureRequest.CloseTreasureBox(slotIdx);

                TaskMgr.instance.FinishTask("SealTreasureBox", 1);
            }
        }

        public void Enchant(UIChestSlot slot)
        {
            if (slot.Content == null || !slot.Content.IsSealed || slot.Content.HasBuff)
                return;

            slot.AddBuff(TreasureSystem.instance.GetBuff(_buffIdx));
            int slotIdx = _slots.FindIndex(x => x == slot);
            Simemes.Request.TreasureRequest.Enchant(slotIdx, _buffIdx);

            _onEnchant?.Invoke();

            TaskMgr.instance.FinishTask("Enchant", 1);

            DisableEnchantMode();
        }

        // �}�Ҫ��]�Ҧ��A�n�]�w���]��buffIdx
        public void EnableEnchantMode(int buffIdx, System.Action onEnchant = null)
        {
            _onEnchant = onEnchant;

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

        public bool AddChest(int treasureBoxID)
        {
            var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(treasureBoxID);
            if (treasureBoxConfig == null)
                return false;

            if (_selectedSlot == null)
            {
                
                foreach(var slot in _slots)
                {
                    if(slot.Content == null && !slot.Locked)
                    {
                        _selectedSlot = slot;
                        break;
                    }
                }
            }

            if (_selectedSlot != null)
            {
                int slotIdx = _slots.FindIndex(x => x == _selectedSlot);
                var treasureBox = new TreasureBox(treasureBoxConfig);
                _selectedSlot.SetBox(treasureBox);
                _selectedSlot = null;

                Simemes.Request.TreasureRequest.AddTreasureBox(slotIdx, treasureBoxID);

                return true;
            }

            return false;
        }

        public bool TryGetEmptySlot(out UIChestSlot slot)
        {
            for (int i = 0; i < _slots.Count; ++i)
            {
                UIChestSlot s = _slots[i];
                if (!s.Locked && s.Content == null)
                {
                    slot = s;
                    return true;
                }
            }

            slot = null;
            return false;
        }

        private void RefreshSlotLocks(Tier.TierData tierData)
        {
            int chestCount = GameManager.instance.PlayerProfile.TierData.ChestSlot;
            for (int i = 0; i < chestCount; ++i)
                _slots[i].SetLock(false);

            for (int i = chestCount; i < _slots.Count; ++i)
                _slots[i].SetLock(true);
        }

        private void RefreshSlots()
        {
            var list = TreasureSystem.instance.GetTreasureBoxes();
            int chestCount = GameManager.instance.PlayerProfile.TierData.ChestSlot;
            for (int i = 0; i < chestCount; ++i)
                _slots[i].Clear(false);
            for (int i = chestCount; i < _slots.Count; ++i)
                _slots[i].Clear(true);

            foreach(var box in list)
            {
                var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(box.ChestID);
                var treasureBox = new TreasureBox(treasureBoxConfig);

                treasureBox.Set(box);

                _slots[box.SlotID].SetBox(treasureBox);
            }
        }

        private void AddTreasureIntoChest(UIChestSlot slot)
        {
            var item = AirDropSystem.instance.GetFirstItem();

            if (item == null)
                return;

            AirDropSystem.instance.RemoveFirstItem();
            LobbyLandscape.instance.PickItem(item, slot.transform.position, (treasure) => slot.SetTreasure(treasure as ITreasure));

            int slotIdx = _slots.FindIndex(x => x == slot);
            Simemes.Request.TreasureRequest.AddTreasureItem(slotIdx, item.ID);
        }

        private void ShowChestInfo(UIChestSlot slot)
        {

        }
    }
}