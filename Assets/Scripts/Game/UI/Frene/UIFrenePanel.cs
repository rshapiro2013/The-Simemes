using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using TMPro;
using Simemes.Tasks;

namespace Simemes.UI.Frene
{
    public class UIFrenePanel : UIPanel
    {
        [SerializeField] private List<UIFreneSlot> _freneSlots;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Canvas _searchPanel;

        private static List<FreneData> _datas = new List<FreneData>();

        public void Open()
        {
            // 初始化通知欄位
            for (int i = 0; i < _freneSlots.Count; ++i)
            {
                bool active = i < _datas.Count;
                UIFreneSlot slot = _freneSlots[i];
                slot.gameObject.SetActive(active);
                if (active)
                    slot.Set(_datas[i]);
            }

            _count.text = _datas.Count.ToString();
        }

        protected override void OnHidePanel()
        {
            base.OnHidePanel();
            _searchPanel.enabled = false;
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            // 沒有通知資料，隨機產生
            if (_datas.Count == 0)
            {
                for (int i = 0; i < 9; ++i)
                {
                    var taskData = new FreneData() { Name = "displayname......", Count = Random.Range(50, 1000000000) };
                    _datas.Add(taskData);
                }
                _datas.Sort((x, y) => y.Count.CompareTo(x.Count));
            }

            Open();
        }
    }
}
