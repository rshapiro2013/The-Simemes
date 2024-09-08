using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Core.UI;
using Simemes.Treasures;

namespace Simemes.UI
{
    public class UIChestSlot : MonoBehaviour
    {
        [SerializeField]
        private Image _treasureBoxImage;

        [SerializeField]
        private UITimer _timer;

        [SerializeField]
        private List<UISpriteAnim> _capacitySlots;

        [SerializeField]
        private GameObject _obj_Lock;

        [SerializeField]
        private GameObject _obj_Add;

        [SerializeField]
        private GameObject _obj_Timer;

        [SerializeField]
        private GameObject _obj_Buff;

        [SerializeField]
        private GameObject _obj_Capacity;

        [SerializeField]
        private UIHoldButton _holdButton;

        public ITreasureBox Content { get; private set; }

        public bool Locked { get; private set; }

        public void Init(ChestData chestData)
        {
            Locked = chestData.IsLocked;

            UpdateState();
        }

        // ��m�_�c
        public void SetBox(ITreasureBox box)
        {
            Content = box;

            UpdateState();
        }

        public bool SetTreasure(ITreasure treasure)
        {
            if (!Content.TryAdd(treasure))
                return false;

            Content.Add(treasure, AirDrop.AirDropSystem.Now);
            UpdateState();
            return true;
        }

        // ���W�_�c�}�l�˼�
        public void Seal()
        {
            Content.Seal();
            _timer.UpdateTime(Content.RemainTime);

            UpdateState();
        }

        // �]�w�O�_�W��
        public void SetLock(bool locked)
        {
            Locked = locked;
            UpdateState();
        }

        public void AddBuff(ITreasureBuff buff)
        {
            Content.AddBuff(buff);
            UpdateState();
        }

        // ��s��l���A
        private void UpdateState()
        {
            _treasureBoxImage.enabled = Content != null;
            if (Content != null)
                _treasureBoxImage.sprite = Content.GetSprite();

            _obj_Lock.SetActive(Locked);
            _obj_Add.SetActive(!Locked && Content == null);

            _obj_Timer.SetActive(Content != null && Content.IsSealed);

            _obj_Buff.SetActive(Content != null && Content.HasBuff);

            _holdButton.ShowProgressBar(Content != null && Content.State != TreasureBoxState.Closed);

            UpdateCapacity();
        }

        private void ObtainTreasure()
        {
            Content.Obtain();
            Content = null;

            UpdateState();
        }

        private void UpdateCapacity()
        {
            _obj_Capacity.SetActive(Content != null);

            if (Content == null)
                return;

            int capacity = Content.Capacity;
            for (int i = 0; i < capacity; ++i)
            {
                _capacitySlots[i].gameObject.SetActive(true);
                _capacitySlots[i].SetSprite(i < Content.ItemWeight ? 1 : 0);
            }

            for (int i = capacity; i < _capacitySlots.Count; ++i)
                _capacitySlots[i].gameObject.SetActive(false);

        }

        private void Update()
        {
            if(Content != null)
            {
                bool timesUp = !Content.Update();

                _timer.UpdateTime(Content.RemainTime);

                if (timesUp)
                    ObtainTreasure();
            }
        }
    }
}