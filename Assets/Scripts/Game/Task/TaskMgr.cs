using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;

namespace Simemes.Tasks
{
    public class TaskMgr : MonoSingleton<TaskMgr>
    {
        [SerializeField]
        private List<TaskConfig> _taskConfigs;

        private List<TaskProgress> _taskProgress;

        private readonly List<TaskConfig> _dailyTaskConfig = new List<TaskConfig>();
        private readonly List<TaskConfig> _memeTaskConfig = new List<TaskConfig>();

        private readonly List<TaskData> _dailyTasks = new List<TaskData>();
        private readonly List<TaskData> _memeTasks = new List<TaskData>();

        public event System.Action OnFinishTask;

        public async Task Init(List<TaskProgress> progressSaveData)
        {
            InitConfig();

            _taskProgress = progressSaveData;

            if (_taskProgress == null || _taskProgress.Count == 0)
                InitTasks();
            else
            {
                foreach (var progress in _taskProgress)
                {
                    var config = _taskConfigs.Find(x => x.ID == progress.ID);
                    if (config == null)
                        continue;

                    var taskData = new TaskData(config, progress);

                    if (config.Type == TaskConfig.TaskType.Daily)
                        _dailyTasks.Add(taskData);
                    else
                        _memeTasks.Add(taskData);
                }
            }
        }

        public void FinishTask(string evt, int value)
        {
            foreach (var task in _dailyTasks)
                task.FinishTask(evt, value);

            foreach (var task in _memeTasks)
                task.FinishTask(evt, value);

            OnFinishTask?.Invoke();
            TaskMgr.instance.UpdateTaskData();
        }

        public List<TaskData> GetDailyTasks()
        {
            return _dailyTasks;
        }

        public List<TaskData> GetMemeTasks()
        {
            return _memeTasks;
        }

        public void UpdateTaskData()
        {
            GameManager.instance.SavePlayerData();
        }

        private void InitConfig()
        {
            foreach(var config in _taskConfigs)
            {
                if (config.Type == TaskConfig.TaskType.Daily)
                    _dailyTaskConfig.Add(config);
                else
                    _memeTaskConfig.Add(config);
            }
        }

        private void InitTasks()
        {
            if (_taskProgress == null)
                _taskProgress = new List<TaskProgress>();

            for (int i = 0; i < 3; ++i)
            {
                int taskIdx = Random.Range(0, _dailyTaskConfig.Count);
                var config = _dailyTaskConfig[taskIdx];
                var taskData = new TaskData(config);

                _dailyTasks.Add(taskData);
                _taskProgress.Add(taskData.Progress);
            }

            for (int i = 0; i < 3; ++i)
            {
                var config = _memeTaskConfig[i];
                var taskData = new TaskData(config);

                _memeTasks.Add(taskData);
                _taskProgress.Add(taskData.Progress);
            }

            // 儲存初始化的任務資料
            GameManager.instance.PlayerProfile.TaskProgress = _taskProgress;
            GameManager.instance.SavePlayerData();
        }
    }
}
