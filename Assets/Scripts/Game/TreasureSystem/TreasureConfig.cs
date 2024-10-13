using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    [CreateAssetMenu(fileName = "TreasureItem", menuName = "Simemes/Treasure/TreasureItem")]
    public class TreasureConfig : Simemes.Rewards.RewardConfig
    {
        [SerializeField]
        protected int _weight;

        [SerializeField]
        protected GameObject _prefab;

        public int Weight => _weight;

        public GameObject Prefab => _prefab;
    }
}