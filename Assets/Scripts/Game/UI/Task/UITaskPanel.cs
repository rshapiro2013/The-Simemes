using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Simemes.Tasks;

namespace Simemes.UI.Tasks
{
    public class UITaskPanel : UIPanel
    {
        [SerializeField]
        private List<UITaskSlot> _taskSlots;

        [SerializeField]
        private List<TaskConfig> _tasks;

        private readonly List<TaskData> _taskData = new List<TaskData>();
        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            // 沒有任務資料，隨機產生
            if(_taskData.Count == 0)
            {
                for (int i = 0; i < _taskSlots.Count; ++i)
                {
                    var config = GetRandomTask();
                    var taskData = new TaskData(config);

                    RandomTaskState(taskData);
                    _taskData.Add(taskData);
                }
            }

            // 初始化任務欄位
            for(int i=0;i<_taskSlots.Count;++i)
                _taskSlots[i].Set(_taskData[i]);

        }

        private TaskConfig GetRandomTask()
        {
            int idx = Random.Range(0, _tasks.Count);
            return _tasks[idx];
        }

        private void RandomTaskState(TaskData task)
        {
            // 一半機率開始任務
            if (Random.value < 0.5f)
            {
                task.StartTask();

                // 一半機率完成任務
                if (Random.value < 0.5f)
                {
                    task.Progress.Current = task.Progress.Target;

                    // 一半機率領取獎勵
                    if (Random.value < 0.5f)
                        task.Claim();
                }

            }
        }
    }
}
