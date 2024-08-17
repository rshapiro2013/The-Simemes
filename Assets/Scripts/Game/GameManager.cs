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

        public int Coin => _coin;

        [SerializeField]  private int _coin = 99999;

        private void Start()
        {
            SetCoin(_coin);
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
    }
}
