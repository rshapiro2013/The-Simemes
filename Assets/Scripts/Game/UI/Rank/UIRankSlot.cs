using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Tasks;

namespace Simemes.UI.Notification
{
    public class UIRankSlot : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _name;

        [SerializeField]
        private TextMeshProUGUI _count;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private RankData _rank;

        public RankData Rank => _rank;


        public void Set(RankData data)
        {
            _rank = data;

            //_icon.sprite = data.Icon;
            _name.text = data.Name;
            _count.text = data.Count.ToString("N0");
        }
    }
}