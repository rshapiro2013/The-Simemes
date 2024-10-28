using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Rewards;

namespace Simemes.Tasks
{
    public class TaskEventData
    {
        private TaskEventConfig _config;

        private TaskEventProgress _progress;

        private readonly List<TaskData> _tasks = new List<TaskData>();

        public TaskEventConfig Config => _config;

        public List<TaskData> Tasks => _tasks;

        public TaskEventProgress Progress => _progress;

        public bool Finished
        {
            get
            {
                foreach(var task in _tasks)
                {
                    if (!task.Claimed)
                        return false;
                }

                return true;
            }
        }
        public bool Started => true;

        public bool Claimed => _progress.Claimed;

        public TaskEventData(TaskEventConfig config, TaskEventProgress progress = null)
        {
            _config = config;

            if (progress != null)
            {
                _progress = progress;
            }
            else
            {
                _progress = new TaskEventProgress();
                _progress.EventID = _config.ID;
                _progress.Claimed = false;
            }
        }

        public void AddTask(TaskData task)
        {
            _tasks.Add(task);
            _progress.Progress.Add(task.Progress);
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
