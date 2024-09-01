using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Tasks
{
    [CreateAssetMenu(fileName = "TaskConfig", menuName = "Simemes/Task/TaskConfig")]
    public class TaskConfig : ScriptableObject
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private string _name;

        [SerializeField]
        private string _reward;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private int _targetValue;

        public int ID => _id;
        public string Name => _name;
        public string Reward => _reward;
        public Sprite Icon => _icon;

        public int TargetValue => _targetValue;

    }
}
