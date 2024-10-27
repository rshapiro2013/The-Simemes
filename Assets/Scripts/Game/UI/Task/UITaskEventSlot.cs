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
        private GameObject _state_Claimed;

        private TaskEventData _taskEvent;

        public TaskEventData TaskEvent => _taskEvent;


        public void Set(TaskEventData data)
        {
            _taskEvent = data;

            _taskIcon.sprite = data.Config.Image;

            _taskName.text = data.Config.Name;

            if (_desc != null)
                _desc.text = data.Config.Desc;

            var reward = data.Config.Reward;
            _reward.text = RewardMgr.instance.GetRewardText(reward.ID, reward.Count);

            UpdateState();
        }

        public void Claim()
        {
            if (!_taskEvent.Finished || _taskEvent.Claimed)
                return;

            _taskEvent.Claim();
            UpdateState();
        }

        private void UpdateState()
        {
            if (_button_Claim != null)
                _button_Claim.gameObject.SetActive(!_taskEvent.Claimed && _taskEvent.Finished);

            _state_Claimed.SetActive(_taskEvent.Claimed);
        }
    }
}