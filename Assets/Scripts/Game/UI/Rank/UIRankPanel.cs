using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Simemes.Tasks;

namespace Simemes.UI.Rank
{
    public class UIRankPanel : UIPanel
    {
        [SerializeField] private List<UIRankSlot> _rankSlots;

        private static List<RankData> _rankData = new List<RankData>();

        public void Open()
        {
                // 初始化通知欄位
            for (int i = 0; i < _rankSlots.Count; ++i)
            {
                bool active = i < _rankData.Count;
                UIRankSlot slot = _rankSlots[i];
                slot.gameObject.SetActive(active);
                if (active)
                    slot.Set(_rankData[i]);
            }
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            // 沒有通知資料，隨機產生
            if (_rankData.Count == 0)
            {
                for (int i = 0; i < 9; ++i)
                {
                    var taskData = new RankData() { Name = "displayname......", Count = Random.Range(50, 1000000000) };
                    _rankData.Add(taskData);
                }
                _rankData.Sort((x, y) => y.Count.CompareTo(x.Count));
            }

            Open();
        }
    }
}
