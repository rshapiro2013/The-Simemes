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
        protected Sprite _image_Opened;

        [SerializeField]
        protected Sprite _image_Closed;

        public int ID => _id;
        public int Capacity => _capacity;
        public int CoolDown => _coolDown;

        public Sprite GetSprite(bool isSealed)
        {
            if (isSealed)
                return _image_Closed;
            else
                return _image_Opened;
        }
    }
}