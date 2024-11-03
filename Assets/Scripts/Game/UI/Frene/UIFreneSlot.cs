using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Tasks;
using Simemes.Frene;
using Simemes.Treasures;

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
        private UIFrenePanel _uIFrenePanel;

        [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField]
        private Transform _contentRoot;

        [SerializeField]
        private UITreasureIcon _sourceIterm;

        private FreneData _data;
        private string _freneID;
        private int _index;
        private List<UITreasureIcon> _pools = new List<UITreasureIcon>();

        public FreneData Frene => _data;

        private void Awake()
        {
            _scrollRect.velocity = Vector2.zero;
            _scrollRect.horizontalNormalizedPosition = 0f;
        }

        public void Set(FreneData data, int index, string freneID = "")
        {
            _scrollRect.horizontalNormalizedPosition = 0f;
            _data = data;
            _index = index;
            _freneID = freneID;

            //_icon.sprite = data.Icon;
            _name.text = data.name;
            _count.text = data.coinAmount.ToString("N0");

            if (_pools.Count < data.items.Count)
            {
                int count = data.items.Count - _pools.Count;
                for (int i = 0; i < count; ++i)
                {
                    UITreasureIcon item = Instantiate(_sourceIterm, _contentRoot);
                    _pools.Add(item);
                }
            }

            TreasureSystem treasureSys = TreasureSystem.instance;
            for (int i = 0; i < data.items.Count; ++i)
            {
                UITreasureIcon item = _pools[i];
                if (int.TryParse(data.items[i], out int id))
                {
                    TreasureConfig treasure = treasureSys.GetTreasureConfig(id);
                    item.Set(treasure.Image, treasure.Name);
                    item.gameObject.SetActive(true);
                }
                else
                    item.gameObject.SetActive(false);
            }
        }

        public void Visit()
        {
            UIStolenView.FreneID = _freneID;
            _uIFrenePanel.Visit(_data);
        }
    }
}