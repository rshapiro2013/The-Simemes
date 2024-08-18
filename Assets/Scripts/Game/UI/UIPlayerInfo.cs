using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;


namespace Simemes.UI
{
    public class UIPlayerInfo : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private Slider _expBar;
        [SerializeField] private TextMeshProUGUI _level;

        private float _current = 0;
        private float _target = 0;

        private int _currentLevel;

        private void Awake()
        {
            if (GameManager.instance)
            {
                GameManager.instance.OnSetExp += SetExp;
                GameManager.instance.OnExpChange += OnExpChange;
                GameManager.instance.OnSetLevel += SetLevel;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.instance)
            {
                GameManager.instance.OnSetExp -= SetExp;
                GameManager.instance.OnExpChange -= OnExpChange;
            }
        }

        private void UpdateExpBar(float value)
        {
            _expBar.value = value;
        }

        private void UpdateLevel(int level)
        {
            _level.text = level.ToString();
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
            // 目標數值與當前數值一致，沒有動畫
            if (_current == _target)
                return;

            // 目標數值改變，播放數值增加動畫
            _current += _speed * Time.deltaTime;

            // 如果數值超過1(連續生好幾級)
            if(_current >= 1.0f)
            {
                _current -= 1.0f;
                _target -= 1.0f;

                // 升級
                SetLevel(_currentLevel + 1);
            }

            // 到達目標數值
            if (_current > _target)
                _current = _target;

            UpdateExpBar(_current);
        }
    }
}