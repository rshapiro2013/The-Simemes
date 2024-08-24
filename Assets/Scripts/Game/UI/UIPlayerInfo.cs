using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;
using Simemes.Profile;

namespace Simemes.UI
{
    public class UIPlayerInfo : MonoBehaviour
    {
        [SerializeField] 
        private float _speed = 1;

        [SerializeField] 
        private Slider _expBar;

        [SerializeField] 
        private TextMeshProUGUI _expText;

        [SerializeField] 
        private TextMeshProUGUI _level;

        [SerializeField]
        private TextMeshProUGUI _name;

        [SerializeField]
        private TextMeshProUGUI _title;

        private float _current = 0;
        private float _target = 0;

        private int _currentLevel;

        private void Awake()
        {
            if (GameManager.instance)
            {
                var playerProfile = GameManager.instance.PlayerProfile;

                playerProfile.OnSetExp += SetExp;
                playerProfile.OnExpChange += OnExpChange;
                playerProfile.OnSetLevel += SetLevel;
                playerProfile.OnUpdateTierData += UpdateTierData;
            }
        }

        private void Start()
        {
            UpdatePlayerInfo(GameManager.instance.PlayerProfile);
        }

        private void OnDestroy()
        {
            if (GameManager.instance)
            {
                var playerProfile = GameManager.instance.PlayerProfile;

                playerProfile.OnSetExp -= SetExp;
                playerProfile.OnExpChange -= OnExpChange;
                playerProfile.OnSetLevel -= SetLevel;
                playerProfile.OnUpdateTierData -= UpdateTierData;
            }
        }

        private void UpdateExpBar(float value)
        {
            _expBar.value = value;
        }

        private void UpdateLevel(int level)
        {
            _level.text = level.ToString();
            _title.text = GameManager.instance.PlayerProfile.TierData.Title;
        }

        private void UpdateExp(int exp, int maxExp)
        {
            _expText.text = $"{exp}/{maxExp}";
        }

        private void SetLevel(int level)
        {
            _currentLevel = level;
            UpdateLevel(_currentLevel);
        }

        private void SetExp(int value)
        {
            _current = value;
            _target = value;

            UpdateExpBar(_current);
        }

        public void OnExpChange(float value)
        {
            _target += value;
        }

        private void Update()
        {
            // �ؼмƭȻP��e�ƭȤ@�P�A�S���ʵe
            if (_current == _target)
                return;

            // �ؼмƭȧ��ܡA����ƭȼW�[�ʵe
            _current += _speed * Time.deltaTime;

            // �p�G�ƭȶW�L1(�s��ͦn�X��)
            if(_current >= 1.0f)
            {
                _current -= 1.0f;
                _target -= 1.0f;

                // �ɯ�
                SetLevel(_currentLevel + 1);
            }

            // ��F�ؼмƭ�
            if (_current > _target)
                _current = _target;

            UpdateExpBar(_current);
        }

        private void UpdatePlayerInfo(PlayerProfile profile)
        {
            _name.text = profile.UserName;
            _title.text = profile.TierData.Title;

            UpdateLevel(profile.Level);
            UpdateExp(profile.Exp, profile.MaxExp);
        }

        private void UpdateTierData(Tier.TierData tierData)
        {
            _title.text = tierData.Title;
        }
    }
}