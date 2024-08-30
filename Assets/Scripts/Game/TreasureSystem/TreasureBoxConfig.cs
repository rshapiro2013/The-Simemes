using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    [CreateAssetMenu(fileName ="TreasureBox", menuName ="Simemes/Treasure/TreasureBox")]
    public class TreasureBoxConfig : ScriptableObject
    {
        [SerializeField]
        protected int _id;

        [SerializeField]
        protected int _capacity;

        [SerializeField]
        protected int _coolDown;

        [SerializeField]
        protected Sprite[] _images;

        public int ID => _id;
        public int Capacity => _capacity;
        public int CoolDown => _coolDown;

        public Sprite GetSprite(TreasureBoxState state)
        {
            return _images[(int)state];
        }
    }
}