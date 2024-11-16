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
        private Dictionary<int, TaskEventProgress> _taskEventProgress;

        private readonly Dictionary<int, List<TaskData>> _tasks = new Dictionary<int, List<TaskData>>();
        private readonly Dictionary<int, List<TaskConfig>> _taskConfigDict = new Dictionary<int, List<TaskConfig>>();

        private readonly Dictionary<int, TaskEventData> _taskEvents = new Dictionary<int, TaskEventData>();
        private readonly Dictionary<int, TaskEventConfig> _taskEventConfigDict = new Dictionary<int, TaskEventConfig>();

        private readonly List<TaskData> _queryTasks = new List<TaskData>();

        public event System.Action OnFinishTask;
        public event System.Action OnUpdateTask;

        public async Task Init(List<TaskProgress> progressSaveData, Dictionary<int, TaskEventProgress> taskEventProgress)
        {
            InitConfig();

            _taskProgress = progressSaveData;
            _taskEventProgress = taskEventProgress;

            InitTaskEventData();
            InitTaskData();         
        }

        public void FinishTask(string evt, int value)
        {
            foreach(var list in _tasks)
            {
                foreach (var task in list.Value)
                    task.FinishTask(evt, value);
            }

            foreach(var eventData in _taskEvents)
            {
                foreach (var task in eventData.Value.Tasks)
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

        public List<TaskData> GetNewTasks()
        {
            _queryTasks.Clear();

            foreach(var taskGroup in _tasks)
            {
                foreach(var task in taskGroup.Value)
                {
                    if (task.Progress.IsNew)
                        _queryTasks.Add(task);
                }
            }

            return _queryTasks;
        }

        public void UpdateTaskData()
        {
            GameManager.instance.SavePlayerData();
            OnUpdateTask?.Invoke();
        }

        public Dictionary<int, TaskEventData> GetTaskEvents()
        {
            return _taskEvents;
        }

        private void InitConfig()
        {
            foreach(var config in _taskConfigs)
            {
                AddTaskConfig(config);
            }

            foreach(var config in _taskEventConfigs)
            {
                AddTaskEventConfig(config);
            }
        }

        private void InitTaskData()
        {
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

                    AddTask((int)taskData.Config.Type, taskData);
                }
            }
        }

        private void InitTasks()
        {
            if (_taskProgress == null)
                _taskProgress = new List<TaskProgress>();


            InitTasks((int)TaskConfig.TaskType.Social);
            InitTasks((int)TaskConfig.TaskType.Meme);

            // 儲存初始化的任務資料
            GameManager.instance.PlayerProfile.TaskProgress = _taskProgress;

            GameManager.instance.SavePlayerData();
        }

        private void InitTaskEventData()
        {
            if (_taskEventProgress == null)
                _taskEventProgress = new Dictionary<int, TaskEventProgress>();

            foreach (var config in _taskEventConfigDict)
            {
                var eventProgress = GetEventProgress(config.Key);
                var eventData = CreateTaskEventData(config.Value, eventProgress);
            }

            GameManager.instance.PlayerProfile.TaskEventProgress = _taskEventProgress;
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

        private void AddTaskEventConfig(TaskEventConfig config)
        {
            int id = config.ID;
            _taskEventConfigDict[id] = config;
        }

        private TaskEventData GetTaskEventData(int eventTaskID)
        {
            _taskEvents.TryGetValue(eventTaskID, out var eventData);
            if (eventData == null)
            {
                _taskEventConfigDict.TryGetValue(eventTaskID, out var eventConfig);
                if (eventConfig == null)
                    return null;

                eventData = new TaskEventData(eventConfig);
                _taskEvents[eventTaskID] = eventData;
            }

            return eventData;
        }

        private TaskEventData CreateTaskEventData(TaskEventConfig eventConfig, TaskEventProgress eventProgress = null)
        {
            var eventData = new TaskEventData(eventConfig, eventProgress);

            if(eventProgress == null)
            {
                eventProgress = eventData.Progress;
                _taskEventProgress[eventProgress.EventID] = eventProgress;
            }

            _taskEvents[eventConfig.ID] = eventData;

            foreach(var task in eventConfig.Tasks)
            {
                var taskProgress = eventProgress.Progress.Find(x => x.ID == task.ID);
                var taskData = new TaskData(task, taskProgress);

                eventData.AddTask(taskData);
                if(taskProgress == null)
                {
                    taskProgress = taskData.Progress;
                    eventData.Progress.Progress.Add(taskProgress);
                }
            }

            return eventData;
        }

        private TaskEventProgress GetEventProgress(int id)
        {
            _taskEventProgress.TryGetValue(id, out var progress);
            return progress;
        }
    }
}
