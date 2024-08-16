using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes.AirDrop
{
    public class AirDropSystem : MonoSingleton<AirDropSystem>
    {
        [SerializeField]
        private long _dropInterval;

        private long _lastCheckTime;

        // ÀË¬dªÅ§ë
        public void CheckAirDrop()
        {
            long now = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            if (_lastCheckTime == 0)
            {
                _lastCheckTime = now;
                return;
            }

            if(now - _lastCheckTime >= _dropInterval)
            {
                int times = (int)((now - _lastCheckTime) / _dropInterval);
                for (int i = 0; i < times; ++i)
                    SpawnRandomItem();
            }
        }

        private void SpawnRandomItem()
        {

        }
    }
}
