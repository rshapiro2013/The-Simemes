using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    [System.Serializable]
    public class ChestData
    {
        public bool IsLocked;
        public int ChestID;
        public int TreasureID;
        public long EndTime;
    }
}
