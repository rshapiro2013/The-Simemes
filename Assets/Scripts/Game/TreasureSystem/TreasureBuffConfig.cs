using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Inventory;

namespace Simemes.Treasures
{
    public class TreasureBuffConfig : TreasureConfig, ITreasureBuff
    {
        public override void Obtain(int count = 1)
        {
            ItemMgr.instance.AddItem(_id, count);
        }

        public virtual void Init(ITreasureBox treasureBox)
        {

        }

        public virtual void TriggerObtain(ITreasureBox treasureBox)
        {

        }

        public virtual void TriggerSteal(ITreasureBox treasureBox)
        {

        }
    }
}
