using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public class Treasure : ITreasure, ILandscapeItem
    {
        protected TreasureConfig _config;

        public int ID => _config.ID;
        public int Weight => _config.Weight;
        public long CreationTime { get; protected set; }
        public Sprite Image => _config.Image;
        public GameObject Prefab => _config.Prefab;

        public Treasure(TreasureConfig config, long currentTime)
        {
            _config = config;
            CreationTime = currentTime;
        }

        public void Obtain()
        {
            _config.Obtain();
        }
    }
}
