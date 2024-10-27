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

        [SerializeField]
        private List<TaskEventConfig> _taskEventConfigs;

        private List<TaskProgress> _taskProgress;

        private readonly Dictionary<int, List<TaskData>> _tasks = new Dictionary<int, List<TaskData>>();
        private readonly Dictionary<int, List<TaskConfig>> _taskConfigDict = new Dictionary<int, List<TaskConfig>>();

        private readonly List<TaskEventData> _taskEvents = new List<TaskEventData>();

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

                    if (taskData.Config.Type == TaskConfig.TaskType.Event)
                        AddTask(progress.EventID, taskData);
                    else
                        AddTask((int)taskData.Config.Type, taskData);
                }
            }
        }

        public void FinishTask(string evt, int value)
        {
            foreach(var list in _tasks)
            {
                foreach (var task in list.Value)
                    task.FinishTask(evt, value);
            }

            OnFinishTask?.Invoke();
            TaskMgr.instance.UpdateTaskData();
        }

        public List<TaskData> GetTasks(int type)
        {
            _tasks.TryGetValue(type, out var tasks);
            return tasks;
        }

        public void UpdateTaskData()
        {
            GameManager.instance.SavePlayerData();
        }

        public List<TaskEventData> GetTaskEvents()
        {
            return null;
        }

        private void InitConfig()
        {
            foreach(var config in _taskConfigs)
            {
                AddTaskConfig(config);
            }
        }

        private void InitTasks()
        {
            if (_taskProgress == null)
                _taskProgress = new List<TaskProgress>();

            InitTasks((int)TaskConfig.TaskType.New);
            InitTasks((int)TaskConfig.TaskType.Social);
            InitTasks((int)TaskConfig.TaskType.Meme);

            // 儲存初始化的任務資料
            GameManager.instance.PlayerProfile.TaskProgress = _taskProgress;
            GameManager.instance.SavePlayerData();
        }

        private void InitTaskEvents()
        {
            foreach (var taskEvent in _taskEventConfigs)
                AddTaskEvent(taskEvent);
        }

        private void InitTasks(int type)
        {
            _taskConfigDict.TryGetValue(type, out var configs);
            if (configs == null || configs.Count == 0)
                return;

            for (int i = 0; i < 3; ++i)
            {
                int taskIdx = Random.Range(0, configs.Count);
                var config = configs[taskIdx];
                var taskData = new TaskData(config);

                AddTask((int)taskData.Config.Type, taskData);
                _taskProgress.Add(taskData.Progress);
            }
        }

        private void AddTask(int type, TaskData data)
        {
            _tasks.TryGetValue(type, out var list);
            if(list == null)
            {
                list = new List<TaskData>();
                _tasks[type] = list;
            }

            list.Add(data);
        }

        private void AddTaskConfig(TaskConfig config)
        {
            int type = (int)config.Type;
            _taskConfigDict.TryGetValue(type, out var list);
            if (list == null)
            {
                list = new List<TaskConfig>();
                _taskConfigDict[type] = list;
            }

            list.Add(config);
        }

        private void AddTaskEvent(TaskEventConfig taskEvent)
        {
            var taskEventData = new TaskEventData(taskEvent);

            int eventID = taskEvent.ID;

            foreach(var task in taskEvent.Tasks)
            {
                TaskData taskData = new TaskData(task);
                taskData.Progress.EventID = eventID;

                AddTask(eventID, taskData);

                _taskProgress.Add(taskData.Progress);
            }
        }
    }
}
