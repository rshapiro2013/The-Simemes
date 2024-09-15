using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Tasks;

namespace Simemes.UI.Frene
{
    public class UIFreneSlot : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _name;

        [SerializeField]
        private TextMeshProUGUI _count;

        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private FreneData _data;

        public FreneData Frene => _data;


        public void Set(FreneData data)
        {
            _data = data;

            //_icon.sprite = data.Icon;
            _name.text = data.Name;
            _count.text = data.Count.ToString("N0");
        }
    }
}