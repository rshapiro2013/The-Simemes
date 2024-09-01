using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Tasks
{
    public class TaskData
    {
        public class TaskProgress
        {
            public int Current;
            public int Target;
        }

        [SerializeField]
        private bool _claimed;

        [SerializeField]
        private bool _started;

        private TaskConfig _config;

        private TaskProgress _progress = new TaskProgress();

        public TaskConfig Config => _config;
        public TaskProgress Progress => _progress;

        public bool Claimed => _claimed;
        public bool Finished => _progress.Current >= _progress.Target;
        public bool Started => _started;

        public TaskData(TaskConfig config)
        {
            _config = config;

            _progress.Current = 0;
            _progress.Target = config.TargetValue;
        }

        public void StartTask()
        {
            _started = true;
        }

        public void Claim()
        {
            if (!Finished)
                return;

            _claimed = true;
        }
    }
}
