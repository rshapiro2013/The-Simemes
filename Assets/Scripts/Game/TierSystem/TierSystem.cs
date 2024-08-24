using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Newtonsoft.Json;

namespace Simemes.Tier
{
    public class TierSystem : MonoSingleton<TierSystem>
    {
        [SerializeField]
        private TextAsset _config;

        private Dictionary<int, TierData> _tierData;

        private const int _tierStart = 1000;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        public void Init()
        {
            _tierData = JsonConvert.DeserializeObject<Dictionary<int, TierData>>(_config.text);
        }

        public TierData GetTierData(int level)
        {
            _tierData.TryGetValue(_tierStart + level, out var data);
            return data;
        }
    }
}