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

        [SerializeField]
        private GameObject _obj_Timer;
        public ITreasureBox Content { get; private set; }

        public bool Locked { get; private set; }

        public void Init(ChestData chestData)
        {
            Locked = chestData.IsLocked;

            UpdateState();
        }

        // 放置寶箱
        public void SetBox(ITreasureBox box)
        {
            Content = box;

            UpdateState();
        }

        public void SetTreasure(ITreasure treasure)
        {
            Content.Add(treasure, AirDrop.AirDropSystem.Now);

            _timer.StartTimer(Content.CoolDown, ObtainTreasure);
            UpdateState();
        }

        // 設定是否上鎖
        public void SetLock(bool locked)
        {
            Locked = locked;
            UpdateState();
        }

        // 更新格子狀態
        private void UpdateState()
        {
            _treasureBoxImage.enabled = Content != null;
            if (Content != null)
                _treasureBoxImage.sprite = Content.GetSprite();

            _obj_Lock.SetActive(Locked);
            _obj_Add.SetActive(!Locked && Content == null);

            _obj_Timer.SetActive(Content != null && !Content.IsEmpty);
        }

        private void ObtainTreasure()
        {
            Content.Obtain();
            Content = null;

            UpdateState();
        }
    }
}