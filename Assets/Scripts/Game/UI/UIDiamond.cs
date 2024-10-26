using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Simemes.UI
{
    public class UIDiamond : MonoBehaviour
    {
        [SerializeField] private float _updateInterval = 0.5f;
        [SerializeField] private TextMeshProUGUI _diamondText;

        private int _currentDiamond = 0;
        private int _targetDiamond = 0;
        private bool _isPlaying;


        private void Awake()
        {
            if (GameManager.instance)
            {
                var playerProfile = GameManager.instance.PlayerProfile;

                playerProfile.OnSetDiamond += SetDiamond;
                playerProfile.OnDiamondChange += OnDiamondChange;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.instance)
            {
                var playerProfile = GameManager.instance.PlayerProfile;

                playerProfile.OnSetDiamond -= SetDiamond;
                playerProfile.OnDiamondChange -= OnDiamondChange;
            }
        }

        private void SetDiamondText(int value)
        {
            _diamondText.text = value.ToString("N0");
        }

        private void SetDiamond(int value)
        {
            _currentDiamond = value;
            SetDiamondText(_currentDiamond);
        }

        public void OnDiamondChange(int coin)
        {
            if(_diamondText!=null)
            {
                StartCoroutine(PlayAddDiamond(coin));
                //_coinText.text = number.ToString("N0");
            }
        }


        private IEnumerator PlayAddDiamond(int value)
        {
            _currentDiamond = GameManager.instance.PlayerProfile.Diamond;
            _targetDiamond = GameManager.instance.PlayerProfile.Diamond + value;

            if (_isPlaying)
                yield break;

            _isPlaying = true;

            while (_isPlaying)
            {
                if (_targetDiamond >= _currentDiamond)
                {
                    SetDiamond(++_currentDiamond);
                    if (_currentDiamond >= _targetDiamond)
                        break;
                }
                else
                {
                    SetDiamond(--_currentDiamond);
                    if (_currentDiamond <= _targetDiamond)
                        break;
                }
                yield return new WaitForSeconds(_updateInterval);
            }

            _isPlaying = false;
        }
    }
}