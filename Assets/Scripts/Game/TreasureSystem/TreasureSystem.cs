using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes.Treasures
{
    public class TreasureSystem : MonoSingleton<TreasureSystem>
    {
        [SerializeField]
        private List<TreasureBoxConfig> _treasureBoxConfigs;

        [SerializeField]
        private List<TreasureConfig> _treasureItems;

        public TreasureBoxConfig GetTreasureBoxConfig(int id)
        {
            return _treasureBoxConfigs.Find(x => x.ID == id);
        }

        public TreasureConfig GetTreasureConfig(int id)
        {
            return _treasureItems.Find(x => x.ID == id);
        }
    }
}