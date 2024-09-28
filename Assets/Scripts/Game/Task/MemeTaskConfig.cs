using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Tasks
{
    [CreateAssetMenu(fileName = "MemeTaskConfig", menuName = "Simemes/Task/Meme Task")]
    public class MemeTaskConfig : TaskConfig
    {
        [SerializeField]
        protected string _url;

        public override void TriggerStart()
        {
            Application.OpenURL(_url);
            TaskMgr.instance.FinishTask(TaskEvent, 1);
        }

        public override void TriggerClaim()
        {

        }

    }
}
