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
        private List<UITaskSlot> _dailyTaskSlots;

        [SerializeField]
        private List<UITaskSlot> _memeTaskSlots;

        [SerializeField]
        private UIElementList _taskEventList;

        [SerializeField]
        private UIElementList _taskList;

        [SerializeField]
        private UIPanel _taskEventInfoPanel;

        [SerializeField]
        private UITaskEventSlot _taskEventInfo;

        private int _currentType;

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            UpdateTaskList();

            UpdateTaskEventList();

            TaskMgr.instance.OnFinishTask += UpdateTaskList;
        }

        protected override void OnHidePanel()
        {
            base.OnHidePanel();

            TaskMgr.instance.OnFinishTask -= UpdateTaskList;
        }

        public void UpdateTaskList()
        {
            SwitchTaskType(_currentType);
        }

        public void UpdateTaskEventList()
        {
            _taskEventList.Clear();

            var taskEventList = TaskMgr.instance.GetTaskEvents();

            if (taskEventList == null || taskEventList.Count == 0)
                return;

            foreach(var evt in taskEventList)
            {
                var element = _taskEventList.CreateElement<UITaskEventSlot>();
                element.Set(evt.Value);
                element.gameObject.SetActive(true);
            }
        }

        public void SwitchTaskType(int idx)
        {
            _currentType = idx;

            UpdateTaskList(idx);
        }

        public void ShowTaskEventInfo(UITaskEventSlot slot)
        {
            _taskEventInfo.Set(slot.TaskEvent);
            _taskEventInfoPanel.EnablePanel(true);
        }

        private void UpdateTaskList(int idx)
        {
            _taskList.Clear();

            var tasks = (idx == 0) ? TaskMgr.instance.GetNewTasks() : TaskMgr.instance.GetTasks(idx - 1);

            if (tasks == null || tasks.Count == 0)
                return;

            // 更新顯示的任務資料
            for (int i = 0; i < tasks.Count; ++i)
            {
                var element = _taskList.CreateElement<UITaskSlot>();
                element.Set(tasks[i]);
                element.gameObject.SetActive(true);
            }
        }
    }
}
