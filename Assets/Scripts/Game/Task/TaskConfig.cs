using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Tasks
{
    [CreateAssetMenu(fileName = "TaskConfig", menuName = "Simemes/Task/TaskConfig")]
    public class TaskConfig : ScriptableObject
    {
        [System.Serializable]
        public class TaskReward
        {
            public int ID;
            public int Count;
        }

        public enum TaskType
        {
            Daily,
            Meme
        }

        [SerializeField]
        protected int _id;

        [SerializeField]
        protected TaskType _type;

        [SerializeField]
        protected string _name;

        [SerializeField]
        protected TaskReward _reward;   

        [SerializeField]
        protected Sprite _icon;

        [SerializeField]
        protected int _targetValue;

        [SerializeField]
        protected string _taskEvent;

        public int ID => _id;
        public TaskType Type => _type;

        public string Name => _name;
        public TaskReward Reward => _reward;
        public Sprite Icon => _icon;

        public int TargetValue => _targetValue;

        public string TaskEvent => _taskEvent;

        public virtual void TriggerStart()
        {

        }

        public virtual void TriggerClaim()
        {

        }

        public virtual int UpdateProgress(int current, int value)
        {
            return current + value;
        }

    }
}
