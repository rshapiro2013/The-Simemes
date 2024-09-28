using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Rewards
{
    [CreateAssetMenu(fileName = "RewardConfig", menuName = "Simemes/Reward/Reward Config")]
    public class RewardConfig : ScriptableObject
    {
        [SerializeField]
        protected int _id;

        [SerializeField]
        protected string _name;

        [SerializeField]
        protected string _rewardFormat;

        public int ID => _id;
        public string Name => _name;

        public string RewardFormat => _rewardFormat;

        public virtual void Obtain(int count = 1)
        {

        }
    }
}
