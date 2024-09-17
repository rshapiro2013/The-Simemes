using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public class TreasureBuffConfig : ScriptableObject, ITreasureBuff
    {
        [SerializeField]
        protected int _id;

        public int ID => _id;

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
