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

        [SerializeField] private UIScrollItems _scrollItems;

        private FreneData _data;
        private string _freneID;
        private int _index;

        public FreneData Frene => _data;

        public void Set(FreneData data, int index, string freneID = "")
        {
            _data = data;
            _index = index;
            _freneID = freneID;

            //_icon.sprite = data.Icon;
            _name.text = data.name;
            _count.text = data.coinAmount.ToString("N0");


            _scrollItems.Set(data.items.Count);

            TreasureSystem treasureSys = TreasureSystem.instance;
            for (int i = 0; i < data.items.Count; ++i)
            {
                UITreasureIcon item = _scrollItems.Pool[i];
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