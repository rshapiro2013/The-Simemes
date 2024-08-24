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

        long StartTime { get; }
        List<ITreasure> Items { get; }

        bool IsEmpty { get; }
        bool IsFull { get; }
        bool IsSealed { get; }

        // 加入寶物
        void Add(ITreasure treasure, long addTime);

        // 檢查是否能加入寶物
        bool TryAdd(ITreasure treasure);

        //關上寶箱
        void Seal();

        // 獲得寶物
        void Obtain();

        Sprite GetSprite();
    }
}
