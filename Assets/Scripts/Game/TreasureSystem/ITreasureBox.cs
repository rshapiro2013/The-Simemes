using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public interface ITreasureBox
    {
        // 箱子ID
        int ID { get; }

        // 箱子容量對應寶物的Weight
        int Capacity { get; }
        // 打開箱子的所需時間
        int CoolDown { get; }

        ITreasure Item { get; }

        bool IsEmpty { get; }

        // 加入寶物
        void Add(ITreasure treasure);
    }
}
