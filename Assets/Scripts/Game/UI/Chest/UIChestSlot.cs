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
        private GameObject _obj_Lock;

        [SerializeField]
        private GameObject _obj_Add;

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

        public void SetTreasure(ITreasure treasure)
        {
            Content.Add(treasure);

            _timer.StartTimer(Content.CoolDown);
            UpdateState();
        }

        // �]�w�O�_�W��
        public void SetLock(bool locked)
        {
            Locked = locked;
            UpdateState();
        }

        // ��s��l���A
        private void UpdateState()
        {
            _treasureBoxImage.enabled = Content != null;
            _obj_Lock.SetActive(Locked);
            _obj_Add.SetActive(!Locked && Content == null);

            _timer.gameObject.SetActive(Content != null && !Content.IsEmpty);
        }

    }
}