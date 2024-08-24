using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Tier;

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

        public TierData TierData { get; private set; }

        public event System.Action<int> OnUpdateExp;
        public event System.Action<int> OnUpdateLevel;
        public event System.Action<TierData> OnUpdateTierData;

        public System.Action<int> OnCoinChange;
        public System.Action<int> OnSetCoin;

        public System.Action<float> OnExpChange;
        public System.Action<int> OnSetExp;

        public System.Action<int> OnSetLevel;

        public void Init()
        {
            Character = "yellow pepe farmer";

            SetCoin(Coin);
            SetExp(Exp);
            SetLevel(Level);
        }

        public void SetCoin(int coin)
        {
            OnSetCoin?.Invoke(coin);
            Coin = coin;
        }

        public void AddCoin(int coin)
        {
            OnCoinChange?.Invoke(coin);
            Coin += coin;
        }

        public void SetExp(int exp)
        {
            OnSetExp?.Invoke(exp);
            Exp = exp;
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
        }
    }
}
