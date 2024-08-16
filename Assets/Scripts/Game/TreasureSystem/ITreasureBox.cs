using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.TreasureSystem
{
    public interface ITreasureBox
    {
        // 箱子容量對應寶物的Weight
        int Capacity { get; }

        // 加入寶物
        void Add(ITreasure treasure);
    }
}
