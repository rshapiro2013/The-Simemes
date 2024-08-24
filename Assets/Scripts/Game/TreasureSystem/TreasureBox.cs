using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public class TreasureBox : ITreasureBox
    {
        protected TreasureBoxConfig _config;
        protected List<ITreasure> _items;
        protected long _startTime;
        protected bool _isSealed;

        protected int _itemWeight;

        public int ID => _config.ID;
        public int Capacity => _config.Capacity;
        public int CoolDown => _config.CoolDown;
        public long StartTime => _startTime;

        public List<ITreasure> Items => _items;
        public bool IsEmpty => _items == null || _items.Count == 0;
        public bool IsFull => _itemWeight >= _config.Capacity;
        public bool IsSealed => _isSealed;

        public TreasureBox(TreasureBoxConfig config)
        {
            _config = config;
            _items = new List<ITreasure>();
        }

        public void Add(ITreasure item, long addTime)
        {
            _items.Add(item);
            _startTime = addTime;
            _itemWeight += item.Weight;
        }

        public bool TryAdd(ITreasure item)
        {
            if (_itemWeight + item.Weight > _config.Capacity)
                return false;

            return true;
        }

        public void Obtain()
        {
            if (_items.Count == 0)
                return;

            foreach (var item in _items)
                item.Obtain();

            _items.Clear();
        }

        public void Seal()
        {
            _isSealed = true;
        }

        public Sprite GetSprite()
        {
            return _config.GetSprite(IsSealed);
        }
    }
}
