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

        [SerializeField]
        protected Sprite _image;

        public int Weight => _weight;
        public Sprite Image => _image;

        public GameObject Prefab => _prefab;
    }
}