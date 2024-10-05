using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Networking;
using Simemes.Tier;
using Newtonsoft.Json;
using Simemes.Tasks;

namespace Simemes.Profile
{
    public class PlayerProfile
    {
        public string UserName;
        public string PhotoUrl;

        public string Character;

        public int Level = 1;

        public int Exp;
        public int MaxExp;

        public int Coin;

        public List<TaskProgress> TaskProgress;

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
        public System.Action<int, int> OnExpChange;
        [JsonIgnore]
        public System.Action<int, int> OnSetExp;
        [JsonIgnore]
        public System.Action<int> OnSetLevel;
        [JsonIgnore]
        public System.Action<PlayerProfile> OnUpdateProfile;
        public void Init()
        {
            SetCoin(Coin);
            SetLevel(Level);
            SetExp(Exp);

            Update();
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

            if (coin > 0)
                TaskMgr.instance.FinishTask("GetGold", coin);
        }

        public bool CheckCoin(int cost)
        {
            return Coin >= cost;
        }

        public void SetExp(int exp)
        {
            OnSetExp?.Invoke(exp, MaxExp);
            UpdateExp(Exp);
        }

        public void AddExp(int exp)
        {
            float expRatioBefore = (float)Exp / MaxExp;
            int levelBefore = Level;

            Exp += exp;

            while (Exp >= MaxExp)
            {
                OnExpChange?.Invoke(MaxExp, MaxExp);
                Exp -= MaxExp;
                LevelUp();
            }

            OnExpChange(Exp, MaxExp);
            UpdateExp(Exp);
        }

        public void SetLevel(int level)
        {
            UpdateLevel(level);
            OnSetLevel?.Invoke(level);
            OnUpdateTierData?.Invoke(TierData);
        }

        public void TriggerLevelUpEvent(int level)
        {
            OnUpdateLevel?.Invoke(level);

            var tierData = TierSystem.instance.GetTierData(level);
            OnUpdateTierData?.Invoke(tierData);
        }

        public void Update()
        {
            OnUpdateProfile?.Invoke(this);
        }

        private void LevelUp()
        {
            UpdateLevel(Level + 1);
        }

        private void UpdateCoin(int coin)
        {
            Coin = coin;

            GameManager.instance.SavePlayerData();
        }

        private void UpdateExp(int exp)
        {
            Exp = exp;

            GameManager.instance.SavePlayerData();

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

            GameManager.instance.SavePlayerData();
            TaskMgr.instance.FinishTask("LevelUp", level);

        }
    }
}
