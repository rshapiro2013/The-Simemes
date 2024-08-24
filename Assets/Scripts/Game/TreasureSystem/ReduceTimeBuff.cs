using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    [CreateAssetMenu(fileName = "ReduceTimeBuff", menuName = "Simemes/TreasureBuff/ReduceTime")]
    public class ReduceTimeBuff : TreasureBuffConfig
    {
        public override void Init(ITreasureBox treasureBox)
        {
            base.Init(treasureBox);

            treasureBox.RemainTime /= 2;
        }
    }
}
