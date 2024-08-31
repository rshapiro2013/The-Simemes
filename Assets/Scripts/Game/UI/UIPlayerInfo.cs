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
        public class ValueUpdate
        {
            public int TargetValue;
            public int MaxValue;
        }

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
        private int _maxValue = 0;

        private int _currentLevel;

        private readonly Queue<ValueUpdate> _valueUpdateQueue = new Queue<ValueUpdate>();

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
            _expBar.value = value / _maxValue;
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

        private void SetExp(int current, int max)
        {
            _current = current;
            _target = current;
            _maxValue = max;

            UpdateExpBar(_current);
            UpdateExp(current, max);
        }

        public void OnExpChange(float value)
        {
            _target += value;
        }

        public void OnExpChange(int targetValue, int maxValue)
        {
            var valueUpdate = new ValueUpdate() { TargetValue = targetValue, MaxValue = maxValue };

            _valueUpdateQueue.Enqueue(valueUpdate);
        }

        private void Update()
        {
            // ��F��e�ʵe��t���ؼмƭȡA�յ۶}�l�U�q�t�X
            if (_current >= _target)
                NextValueUpdate();

            // �S����t
            if (_current >= _target)
                return;

            // �ؼмƭȧ��ܡA����ƭȼW�[�ʵe
            _current += _maxValue * _speed * Time.deltaTime;

            bool reachMax = _current >= _maxValue;
            bool reachTarget = _current >= _target;

            // ��F�ؼмƭ�
            if (reachTarget)
            {
                _current = _target;
                NextValueUpdate();
            }

            // �p�G��F�̤j�ƭ�
            if (reachMax)
            {
                _current = 0;

                // �ɯ�
                SetLevel(_currentLevel + 1);
            }

            UpdateExpBar(_current);
            UpdateExp((int)_current, _maxValue);
        }

        private void UpdatePlayerInfo(PlayerProfile profile)
        {
            _name.text = profile.UserName;
            _title.text = profile.TierData.Title;

            UpdateLevel(profile.Level);
        }

        private void UpdateTierData(Tier.TierData tierData)
        {
            _title.text = tierData.Title;
        }

        private void NextValueUpdate()
        {
            if (_valueUpdateQueue.Count == 0)
                return;

            var valueUpdate = _valueUpdateQueue.Dequeue();
            _target = valueUpdate.TargetValue;

            _maxValue = valueUpdate.MaxValue;
        }
    }
}