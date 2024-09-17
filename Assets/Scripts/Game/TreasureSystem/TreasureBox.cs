using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public class TreasureBox : ITreasureBox
    {
        protected TreasureBoxConfig _config;
        protected List<ITreasure> _items;
        protected ITreasureBuff _buff;

        protected float _remainTime;
        protected long _startTime;
        protected bool _isSealed;

        protected int _itemWeight;

        public int ID => _config.ID;
        public int Capacity => _config.Capacity;
        public int CoolDown => _config.CoolDown;
        public long StartTime => _startTime;
        public float RemainTime { get => _remainTime; set => _remainTime = value; }

        public List<ITreasure> Items => _items;
        public ITreasureBuff Buff => _buff;

        public bool IsEmpty => _items == null || _items.Count == 0;
        public bool IsFull => _itemWeight >= _config.Capacity;
        public bool IsSealed => _isSealed;
        public bool HasBuff => _buff != null;

        public int ItemWeight => _itemWeight;

        public TreasureBoxState State { get; set; }

        public TreasureBox(TreasureBoxConfig config)
        {
            _config = config;
            _items = new List<ITreasure>();
            _remainTime = config.CoolDown;
            State = TreasureBoxState.Opened;
        }

        public void Set(ChestData chestData)
        {
            var treasureSys = TreasureSystem.instance;

            var box = treasureSys.GetTreasureBoxConfig(chestData.ChestID);
            foreach(var itemIdx in chestData.Treasures)
            {
                var item = treasureSys.GetTreasureConfig(itemIdx);
                var treasure = new Treasure(item, chestData.StartTime);
                Add(treasure, chestData.StartTime);
            }

            if (chestData.IsSealed)
                Seal();

            if(chestData.BuffID > 0)
            {
                var buff = treasureSys.GetBuff(chestData.BuffID);
                AddBuff(buff);
            }

            _startTime = chestData.StartTime;
            if (chestData.IsSealed)
                _remainTime = chestData.CoolDown - (AirDrop.AirDropSystem.Now - _startTime);
        }

        public void Add(ITreasure item, long addTime)
        {
            _items.Add(item);
            _startTime = addTime;
            _itemWeight += item.Weight;

            if (IsFull)
                State = TreasureBoxState.Full;
            else
                State = TreasureBoxState.Half;
        }

        public bool TryAdd(ITreasure item)
        {
            if (_itemWeight + item.Weight > _config.Capacity)
                return false;

            return true;
        }

        public void Obtain()
        {
            _buff?.TriggerObtain(this);

            if (_items.Count == 0)
                return;

            foreach (var item in _items)
                item.Obtain();

            _items.Clear();
        }

        public void Seal()
        {
            _isSealed = true;
            State = TreasureBoxState.Closed;
        }

        public Sprite GetSprite()
        {
            return _config.GetSprite(State);
        }

        public void AddBuff(ITreasureBuff buff)
        {
            _buff = buff;
            _buff.Init(this);
        }

        public void RemoveBuff(ITreasureBuff buff)
        {
            if (_buff == buff)
                _buff = null;
        }

        public bool Update()
        {
            if (!IsSealed)
                return true;

            _remainTime -= Time.deltaTime;
            if (_remainTime <= 0)
                return false;

            return true;
        }
    }
}
