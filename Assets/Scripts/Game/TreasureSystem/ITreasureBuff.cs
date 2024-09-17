using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public interface ITreasureBuff
    {
        int ID { get; }
        void Init(ITreasureBox treasureBox);
        void TriggerObtain(ITreasureBox treasureBox);
        void TriggerSteal(ITreasureBox treasureBox);

    }
}