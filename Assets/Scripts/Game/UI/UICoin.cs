using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Simemes.UI
{
    public class UICoin : MonoBehaviour
    {
        [SerializeField] private float _updateInterval = 0.5f;
        [SerializeField] private TextMeshProUGUI _coinText;

        private int _currentCoin = 0;
        private int _targetCoin = 0;
        private bool _isPlaying;


        private void Awake()
        {
            if (GameManager.instance)
            {
                GameManager.instance.OnSetCoin += SetCoin;
                GameManager.instance.OnCoinChange += OnCoinChange;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.instance)
            {
                GameManager.instance.OnSetCoin -= SetCoin;
                GameManager.instance.OnCoinChange -= OnCoinChange;
            }
        }

        private void SetCoinText(int value)
        {
            _coinText.text = value.ToString("N0");
        }

        private void SetCoin(int value)
        {
            _currentCoin = value;
            SetCoinText(_currentCoin);
        }

        public void OnCoinChange(int coin)
        {
            if(_coinText!=null)
            {
                StartCoroutine(PlayAddCoin(coin));
                //_coinText.text = number.ToString("N0");
            }
        }


        private IEnumerator PlayAddCoin(int value)
        {
            _currentCoin = GameManager.instance.Coin;
            _targetCoin = GameManager.instance.Coin + value;

            if (_isPlaying)
                yield break;

            _isPlaying = true;

            while (_isPlaying)
            {
                SetCoin(++_currentCoin);
                if(_currentCoin >= _targetCoin)
                    break;
                yield return new WaitForSeconds(_updateInterval);
            }

            _isPlaying = false;
        }
    }
}