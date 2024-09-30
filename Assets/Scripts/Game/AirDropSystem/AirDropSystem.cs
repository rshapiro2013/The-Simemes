using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Simemes.Treasures;
using Simemes.Landscape;
using Newtonsoft.Json;

namespace Simemes.AirDrop
{
    public class AirDropSystem : MonoSingleton<AirDropSystem>
    {
        public class TreasureData
        {
            public int ItemID;
            public long CreationTime;

            public TreasureData()
            {

            }

            public TreasureData(Treasure treasure)
            {
                ItemID = treasure.ID;
                CreationTime = treasure.CreationTime;
            }
        }

        [SerializeField]
        protected AirDropConfig _airDropConfig;

        [SerializeField]
        private int _dropInterval;

        [SerializeField]
        private int _maxItemCount = 10;

        private long _lastCheckTime;

        private readonly Queue<Treasure> _treasures = new Queue<Treasure>();

        private readonly List<TreasureData> _treasureData = new List<TreasureData>();

        public static long Now => System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        protected virtual void Start()
        {
            LoadData();
            InitialSpawn();

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

            SaveData();
        }

        private void SpawnRandomItem()
        {
            if (_treasures.Count >= _maxItemCount)
                return;

            int itemID = _airDropConfig.GetRandomDropItem();

            SpawnTreasureItem(itemID, Now);
            SaveData();
        }

        private void SpawnTreasureItem(int itemID, long creationTime)
        {
            var config = TreasureSystem.instance.GetTreasureConfig(itemID);

            if (config != null)
            {
                var treasure = new Treasure(config, creationTime);
                SpawnTreasureItem(treasure);
            }
        }
        
        private void SpawnTreasureItem(Treasure treasure)
        {
            _treasures.Enqueue(treasure);

            // 把看得見的物品加到場景中
            LobbyLandscape.instance.AddItem(treasure);
        }

        private void UpdateTime(long time)
        {
            _lastCheckTime = time;
            PlayerPrefs.SetString("LastCheckTime", _lastCheckTime.ToString());
        }

        private void SaveData()
        {
            _treasureData.Clear();

            foreach(var treasure in _treasures)
                _treasureData.Add(new TreasureData(treasure));

            string data = JsonConvert.SerializeObject(_treasureData);
            PlayerPrefs.SetString("AirDropData", data);
        }

        private void LoadData()
        {
            string data = PlayerPrefs.GetString("AirDropData");
            if (string.IsNullOrEmpty(data))
                return;

            _treasureData.Clear();
            JsonConvert.PopulateObject(data, _treasureData);
            foreach(var treasure in _treasureData)
            {
                SpawnTreasureItem(treasure.ItemID, treasure.CreationTime);
            }
        }

        private void InitialSpawn()
        {
            string lastSpawnTimeStr = PlayerPrefs.GetString("LastCheckTime");
            if(!string.IsNullOrEmpty(lastSpawnTimeStr))
            {
                _lastCheckTime = long.Parse(lastSpawnTimeStr);
            }
            else
            {
                _lastCheckTime = Now;
                PlayerPrefs.SetString("LastCheckTime", _lastCheckTime.ToString());
            }

            if (Now - _lastCheckTime >= _dropInterval)
            {
                int times = (int)((Now - _lastCheckTime) / _dropInterval);
                UpdateTime(_lastCheckTime + _dropInterval * times);

                int spawnCount = Mathf.Min(_maxItemCount - _treasures.Count, times);
                for (int i = 0; i < spawnCount; ++i)
                    SpawnRandomItem();
            }

        }
    }
}
