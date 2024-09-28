using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Simemes.Tasks;

namespace Simemes.Poop
{
    public class PoopSystem : MonoSingleton<PoopSystem>
    {
        public System.Action OnSpawnPoop;

        [SerializeField] private int _limitCount = 20;
        [SerializeField] private int _poops = 15;
        [SerializeField] private float _testSpawnTime = 10;

        protected long _lastCollectTime;

        public int PoopCount => _poops;

        protected override void Awake()
        {
            base.Awake();

            Init();
            StartCoroutine(DoUpdate());
        }

        protected virtual IEnumerator DoUpdate()
        {
            while (true)
            {
                //CheckAirDrop();
                yield return new WaitForSeconds(_testSpawnTime);
                SpawnPoop();
            }
        }

        protected void Init()
        {
            long now = AirDrop.AirDropSystem.Now;
            var lastCollectTimeStr = PlayerPrefs.GetString("LastPoopCollectTime");
            if (string.IsNullOrEmpty(lastCollectTimeStr))
            {
                _lastCollectTime = now;
                PlayerPrefs.SetString("LastPoopCollectTime", now.ToString());
            }
            else
                _lastCollectTime = long.Parse(lastCollectTimeStr);

            // 根據上次領取時間補足大便數量
            for (long current = _lastCollectTime; current < now; current += (long)_testSpawnTime)
            {
                if (!SpawnPoop())
                    break;
            }
        }

        protected bool SpawnPoop()
        {
            if (_poops >= _limitCount)
                return false;

            OnSpawnPoop?.Invoke();
            _poops += 1;
            return true;
        }

        public void Collect()
        {
            _poops = 0;

            long now = AirDrop.AirDropSystem.Now;
            PlayerPrefs.SetString("LastPoopCollectTime", now.ToString());

            TaskMgr.instance.FinishTask("CollectPoops", 1);
        }
    }
}