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


        public TreasureBoxConfig GetTreasureBoxConfig(int id)
        {
            return _treasureBoxConfigs.Find(x => x.ID == id);
        }
    }
}