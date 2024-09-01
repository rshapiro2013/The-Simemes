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

            // �S�����ȸ�ơA�H������
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

            // ��l�ƥ������
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
            // �@�b���v�}�l����
            if (Random.value < 0.5f)
            {
                task.StartTask();

                // �@�b���v��������
                if (Random.value < 0.5f)
                {
                    task.Progress.Current = task.Progress.Target;

                    // �@�b���v������y
                    if (Random.value < 0.5f)
                        task.Claim();
                }

            }
        }
    }
}
