using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public System.Action<int> OnCoinChange;
        public System.Action<int> OnSetCoin;

        public System.Action<float> OnExpChange;
        public System.Action<int> OnSetExp;

        public System.Action<int> OnSetLevel;

        public int Coin => _coin;
        public int Exp => _exp;

        public int Level => _level;

        public int MaxExp => _level * 100;

        [SerializeField] private int _coin = 99999;
        [SerializeField] private int _level = 1;
        [SerializeField] private int _exp = 0;

        private void Start()
        {
            SetCoin(_coin);
            SetExp(_exp);
            SetLevel(_level);
        }

        public void SetCoin(int coin)
        {
            OnSetCoin?.Invoke(coin);
            _coin = coin;
        }

        public void AddCoin(int coin)
        {
            OnCoinChange?.Invoke(coin);
            _coin += coin;
        }

        public void SetExp(int exp)
        {
            OnSetExp?.Invoke(exp);
            _exp = exp;
        }

        public void AddExp(int exp)
        {
            float expRatioBefore = (float)_exp / MaxExp;
            int levelBefore = _level;

            _exp += exp;
            
            while (_exp >= MaxExp)
            {
                _exp -= MaxExp;
                LevelUp();
            }

            int levelAdd = _level - levelBefore;
            float expRatio = levelAdd + ((float)_exp / MaxExp - expRatioBefore);

            OnExpChange?.Invoke(expRatio);
        }

        public void SetLevel(int level)
        {
            _level = level;
            OnSetLevel?.Invoke(level);
        }

        private void LevelUp()
        {
            ++_level;
        }
    }
}
