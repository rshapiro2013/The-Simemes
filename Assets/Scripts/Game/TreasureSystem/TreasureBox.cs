using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public class TreasureBox : ITreasureBox
    {
        protected TreasureBoxConfig _config;
        protected ITreasure _item;
        protected long _startTime;

        public int ID => _config.ID;
        public int Capacity => _config.Capacity;
        public int CoolDown => _config.CoolDown;
        public long StartTime => _startTime;

        public ITreasure Item => _item;
        public bool IsEmpty => _item == null;

        public TreasureBox(TreasureBoxConfig config)
        {
            _config = config;
        }

        public void Add(ITreasure item, long addTime)
        {
            _item = item;
            _startTime = addTime;
        }

        public void Obtain()
        {
            _item.Obtain();

            _item = null;
        }

        public Sprite GetSprite()
        {
            return _config.GetSprite(IsEmpty);
        }
    }
}
