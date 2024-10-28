using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Tasks;
using Simemes.Rewards;

namespace Simemes.UI.Tasks
{
    public class UITaskEventSlot : Core.UI.UIElement
    {
        [SerializeField]
        private UITaskPanel _parentPanel;

        [SerializeField]
        private Image _taskIcon;

        [SerializeField]
        private TextMeshProUGUI _taskName;

        [SerializeField]
        private TextMeshProUGUI _desc;

        [SerializeField]
        private TextMeshProUGUI _reward;

        [SerializeField]
        private Button _button_Claim;

        [SerializeField]
        private Button _button_Open;

        [SerializeField]
        private GameObject _state_Claimed;

        [SerializeField]
        private List<UITaskSlot> _taskSlots;

        private TaskEventData _taskEvent;

        public TaskEventData TaskEvent => _taskEvent;

        private void OnDestroy()
        {
            foreach (var taskSlot in _taskSlots)
                taskSlot.OnUpdateState -= UpdateState;

            if (TaskMgr.instance != null)
                TaskMgr.instance.OnUpdateTask -= UpdateState;
        }

        public void Set(TaskEventData data)
        {
            TaskMgr.instance.OnUpdateTask += UpdateState;

            _taskEvent = data;

            _taskIcon.sprite = data.Config.Image;

            _taskName.text = data.Config.Name;

            if (_desc != null)
                _desc.text = data.Config.Desc;

            var reward = data.Config.Reward;
            _reward.text = RewardMgr.instance.GetRewardText(reward.ID, reward.Count);

            if (_taskSlots != null && _taskSlots.Count > 0)
            {
                for (int i = 0; i < data.Tasks.Count; ++i)
                {
                    _taskSlots[i].Set(data.Tasks[i]);
                    _taskSlots[i].OnUpdateState += UpdateState;
                    _taskSlots[i].gameObject.SetActive(true);
                }

                for (int i = data.Tasks.Count; i < _taskSlots.Count; ++i)
                    _taskSlots[i].gameObject.SetActive(false);
            }

            UpdateState();
        }

        public void Claim()
        {
            if (!_taskEvent.Finished || _taskEvent.Claimed)
                return;

            _taskEvent.Claim();
            UpdateState();
        }

        public void ShowInfo()
        {
            _parentPanel.ShowTaskEventInfo(this);
        }

        private void UpdateState()
        {
            if (_button_Claim != null)
                _button_Claim.gameObject.SetActive(!_taskEvent.Claimed && _taskEvent.Finished);

            _state_Claimed.SetActive(_taskEvent.Claimed);
            if (_button_Open != null)
                _button_Open.gameObject.SetActive(!_taskEvent.Claimed);
        }
    }
}