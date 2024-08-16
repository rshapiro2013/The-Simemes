using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    [CreateAssetMenu(fileName ="TreasureBox", menuName ="Simemes/Treasure/TreasureBox")]
    public class TreasureBoxConfig : ScriptableObject, ITreasureBox
    {
        [SerializeField]
        protected int _id;

        [SerializeField]
        protected int _capacity;

        [SerializeField]
        protected int _coolDown;


        protected ITreasure _item;

        public int ID => _id;
        public int Capacity => _capacity;
        public int CoolDown => _coolDown;

        public ITreasure Item => _item;
        public bool IsEmpty => _item == null;
        
        public void Add(ITreasure item)
        {
            _item = item;
        }
    }
}