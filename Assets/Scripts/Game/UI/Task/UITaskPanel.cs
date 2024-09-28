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

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            UpdateTaskList();

            TaskMgr.instance.OnFinishTask += UpdateTaskList;
        }

        protected override void OnHidePanel()
        {
            base.OnHidePanel();

            TaskMgr.instance.OnFinishTask -= UpdateTaskList;
        }

        public void UpdateTaskList()
        {
            var dailTasks = TaskMgr.instance.GetDailyTasks();
            var memeTasks = TaskMgr.instance.GetMemeTasks();

            // 更新顯示的任務資料
            for (int i = 0; i < _dailyTaskSlots.Count; ++i)
                _dailyTaskSlots[i].Set(dailTasks[i]);

            for (int i = 0; i < _memeTaskSlots.Count; ++i)
                _memeTaskSlots[i].Set(memeTasks[i]);
        }
    }
}
