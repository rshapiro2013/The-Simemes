using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Tier;
using Newtonsoft.Json;

namespace Simemes.Profile
{
    public class PlayerProfile
    {
        public string UserName;

        public string Character;

        public int Level = 1;

        public int Exp;
        public int MaxExp;

        public int Coin;

        [JsonIgnore]
        public TierData TierData { get; private set; }

        public event System.Action<int> OnUpdateExp;
        public event System.Action<int> OnUpdateLevel;
        public event System.Action<TierData> OnUpdateTierData;

        [JsonIgnore]
        public System.Action<int> OnCoinChange;
        [JsonIgnore]
        public System.Action<int> OnSetCoin;
        [JsonIgnore]
        public System.Action<float> OnExpChange;
        [JsonIgnore]
        public System.Action<float> OnSetExp;
        [JsonIgnore]
        public System.Action<int> OnSetLevel;

        public void Init()
        {
            SetCoin(Coin);
            SetExp(Exp);
            SetLevel(Level);
        }

        public void SetCoin(int coin)
        {
            OnSetCoin?.Invoke(coin);
            UpdateCoin(coin);
        }

        public void AddCoin(int coin)
        {
            OnCoinChange?.Invoke(coin);
            UpdateCoin(Coin + coin);
        }

        public void SetExp(int exp)
        {
            OnSetExp?.Invoke((float)exp / MaxExp);
            UpdateExp(Exp);
        }

        public void AddExp(int exp)
        {
            float expRatioBefore = (float)Exp / MaxExp;
            int levelBefore = Level;

            Exp += exp;

            while (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                LevelUp();
            }

            UpdateExp(Exp);

            int levelAdd = Level - levelBefore;
            float expRatio = levelAdd + ((float)Exp / MaxExp - expRatioBefore);

            OnExpChange?.Invoke(expRatio);
        }

        public void SetLevel(int level)
        {
            UpdateLevel(level);
            OnSetLevel?.Invoke(level);
        }

        private void LevelUp()
        {
            UpdateLevel(Level + 1);
        }

        private void UpdateCoin(int coin)
        {
            Coin = coin;

            RequestSystem.instance.UploadData<PlayerProfile>("PlayerProfile", this);
        }

        private void UpdateExp(int exp)
        {
            Exp = exp;

            RequestSystem.instance.UploadData<PlayerProfile>("PlayerProfile", this);
        }

        private void UpdateLevel(int level)
        {
            Level = level;

            TierData = TierSystem.instance.GetTierData(Level);

            var nextTierData = TierSystem.instance.GetTierData(Level + 1);

            if (nextTierData != null)
                MaxExp = nextTierData.ExpRequire;
            else
                MaxExp = 0;

            OnUpdateTierData?.Invoke(TierData);

            RequestSystem.instance.UploadData<PlayerProfile>("PlayerProfile", this);
        }
    }
}
