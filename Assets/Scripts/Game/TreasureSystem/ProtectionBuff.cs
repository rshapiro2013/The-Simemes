using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    [CreateAssetMenu(fileName = "ProtectionBuff", menuName = "Simemes/TreasureBuff/Protection")]
    public class ProtectionBuff : TreasureBuffConfig
    {
        public override void TriggerSteal(ITreasureBox treasureBox)
        {
            base.TriggerSteal(treasureBox);

            // �Q���ɵ��������O�@
        }
    }
}
