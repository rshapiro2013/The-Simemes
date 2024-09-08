using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using TMPro;
using Simemes.Profile;

namespace Simemes.UI
{
    public class UILevelUpPanel : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI _level;

        protected override void Awake()
        {
            base.Awake();

            GameManager.instance.PlayerProfile.OnUpdateLevel += OnUpdateLevel;
        }

        protected override void OnDestroy()
        {
            if (GameManager.instance != null)
                GameManager.instance.PlayerProfile.OnUpdateLevel -= OnUpdateLevel;
        }

        private void OnUpdateLevel(int level)
        {
            _level.text = level.ToString();

            EnablePanel(true);
        }
    }
}