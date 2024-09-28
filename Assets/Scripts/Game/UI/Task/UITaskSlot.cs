using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Tasks;
using Simemes.Rewards;

namespace Simemes.UI.Tasks
{
    public class UITaskSlot : MonoBehaviour
    {
        [SerializeField]
        private Image _taskIcon;

        [SerializeField]
        private TextMeshProUGUI _taskName;

        [SerializeField]
        private TextMeshProUGUI _reward;

        [SerializeField]
        private Slider _progressSlider;

        [SerializeField]
        private TextMeshProUGUI _progressText;

        [SerializeField]
        private Button _button_Claim;

        [SerializeField]
        private Button _button_Start;

        [SerializeField]
        private GameObject _state_Claimed;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private TaskData _task;

        public TaskData Task => _task;


        public void Set(TaskData data)
        {
            _task = data;

            _taskIcon.sprite = data.Config.Icon;

            _taskName.text = data.Config.Name;

            var reward = data.Config.Reward;
            _reward.text = RewardMgr.instance.GetRewardText(reward.ID, reward.Count);

            UpdateState();
        }

        public void StartTask()
        {
            _task.StartTask();
            UpdateState();
        }

        public void Claim()
        {
            if (!_task.Finished || _task.Claimed)
                return;

            _task.Claim();
            UpdateState();
        }

        private void UpdateState()
        {
            _progressSlider.gameObject.SetActive(_task.Started && !_task.Finished);

            if (_task.Started)
            {
                _progressSlider.value = (float)_task.Progress.Current / _task.Progress.Target;
                _progressText.text = $"{_task.Progress.Current}/{_task.Progress.Target}";
            }

            _button_Claim.gameObject.SetActive(!_task.Claimed && _task.Finished);
            _button_Start.gameObject.SetActive(!_task.Started);
            _state_Claimed.SetActive(_task.Claimed);

            _canvasGroup.alpha = _task.Claimed ? 0.5f : 1.0f;
        }
    }
}