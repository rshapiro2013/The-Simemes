using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Core.UI
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _timerText;

        private float _seconds;

        private System.Action _onStop;

        private void Reset()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateTime(float seconds)
        {
            _seconds = seconds;
            UpdateTime();
        }

        private void UpdateTime()
        {
            var t = System.TimeSpan.FromSeconds(_seconds);

            string time = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);

            _timerText.text = time;
        }
    }
}
