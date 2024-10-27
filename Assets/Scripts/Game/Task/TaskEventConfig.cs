using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Tasks
{
    [CreateAssetMenu(fileName = "TaskEventConfig", menuName = "Simemes/Task/TaskEventConfig")]
    public class TaskEventConfig : ScriptableObject
    {
        [SerializeField]
        private List<TaskConfig> _tasks;

        [SerializeField]
        private int _id;

        [SerializeField]
        private string _name;

        [TextArea(3, 10)]
        [SerializeField]
        private string _desc;

        [SerializeField]
        private Rewards.RewardData _reward;

        [SerializeField]
        private Sprite _image;

        public int ID => _id;

        public string Name => _name;

        public string Desc => _desc;

        public Rewards.RewardData Reward => _reward;

        public Sprite Image => _image;

        public List<TaskConfig> Tasks => _tasks;
    }
}