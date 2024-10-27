using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Rewards;

namespace Simemes.Tasks
{
    public class TaskEventData
    {
        private TaskEventConfig _config;

        private TaskProgress _progress;

        public TaskEventConfig Config => _config;
        public TaskProgress Progress => _progress;

        public bool Claimed => _progress.Claimed;
        public bool Finished => _progress.Current >= _progress.Target;
        public bool Started => _progress.Started;

        public TaskEventData(TaskEventConfig config, TaskProgress progress = null)
        {
            _config = config;

            if (progress != null)
            {
                _progress = progress;
            }
            else
            {
                _progress = new TaskProgress();
                _progress.ID = _config.ID;
                _progress.Current = 0;
                _progress.Target = config.Tasks.Count;
            }
        }

        public void Claim()
        {
            if (!Finished)
                return;

            RewardMgr.instance.ObtainReward(_config.Reward.ID, _config.Reward.Count);

            _progress.Claimed = true;
            TaskMgr.instance.UpdateTaskData();
        }
    }
}
