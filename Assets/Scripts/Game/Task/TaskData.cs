using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Tasks
{
    public class TaskData
    {
        private TaskConfig _config;

        private TaskProgress _progress;

        public TaskConfig Config => _config;
        public TaskProgress Progress => _progress;

        public bool Claimed => _progress.Claimed;
        public bool Finished => _progress.Current >= _progress.Target;
        public bool Started => _progress.Started;

        public TaskData(TaskConfig config, TaskProgress progress = null)
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
                _progress.Current = config.UpdateProgress(0, 0);
                _progress.Target = config.TargetValue;
            }
        }

        public void StartTask()
        {
            _progress.Started = true;
            _config.TriggerStart();
            TaskMgr.instance.UpdateTaskData();
        }

        public void Claim()
        {
            if (!Finished)
                return;

            _progress.Claimed = true;
            _config.TriggerClaim();
            TaskMgr.instance.UpdateTaskData();
        }

        public void FinishTask(string evt, int value)
        {
            if (!_progress.Started || _progress.Claimed)
                return;

            if (evt == _config.TaskEvent)
                UpdateProgress(value);
        }

        public void UpdateProgress(int value)
        {
            _progress.Current = _config.UpdateProgress(_progress.Current, value);
        }
    }
}
