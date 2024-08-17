using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Simemes.Treasures;
using Simemes.Landscape;

namespace Simemes.AirDrop
{
    public class AirDropSystem : MonoSingleton<AirDropSystem>
    {
        [SerializeField]
        protected AirDropConfig _airDropConfig;

        [SerializeField]
        private int _dropInterval;


        private long _lastCheckTime;

        private readonly Queue<Treasure> _treasures = new Queue<Treasure>();

        public static long Now => System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        protected override void Awake()
        {
            base.Awake();

            //_lastCheckTime = PlayerPrefs.GetInt("LastCheckTime", 0);

            StartCoroutine(DoUpdate());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopAllCoroutines();
        }

        protected virtual IEnumerator DoUpdate()
        {
            while(true)
            {
                CheckAirDrop();
                yield return new WaitForSeconds(1);
            }
        }

        // 檢查空投
        public void CheckAirDrop()
        {
            long now = Now;
            if (_lastCheckTime == 0)
            {
                UpdateTime(now);
                return;
            }

            if(now - _lastCheckTime >= _dropInterval)
            {
                int times = (int)((now - _lastCheckTime) / _dropInterval);
                UpdateTime(_lastCheckTime + _dropInterval * times);

                for (int i = 0; i < times; ++i)
                    SpawnRandomItem();
            }
            
        }

        public Treasure GetFirstItem()
        {
            if (_treasures.Count == 0)
                return null;

            return _treasures.Peek();
        }

        public void RemoveFirstItem()
        {
            if (_treasures.Count == 0)
                return;

            _treasures.Dequeue();
        }

        private void SpawnRandomItem()
        {
            int itemID = _airDropConfig.GetRandomDropItem();

            var config = TreasureSystem.instance.GetTreasureConfig(itemID);

            if (config != null)
            {
                var treasure = new Treasure(config, Now);
                _treasures.Enqueue(treasure);

                // 把看得見的物品加到場景中
                LobbyLandscape.instance.AddItem(treasure);
            }
        }
        
        private void UpdateTime(long time)
        {
            _lastCheckTime = time;
            //PlayerPrefs.SetInt("LastCheckTime", (int)_lastCheckTime);
        }
    }
}
