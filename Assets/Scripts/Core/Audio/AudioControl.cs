using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField]
        private string _id;

        [SerializeField]
        private AudioSource _source;

        private void Awake()
        {
            int mute = PlayerPrefs.GetInt($"{_id}_mute", 0);

            _source.mute = mute != 0;
        }

        public void SetMute(bool mute)
        {
            _source.mute = mute;
            PlayerPrefs.SetInt($"{_id}_mute", mute ? 1 : 0);
        }
    }
}
