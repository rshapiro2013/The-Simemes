using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using TMPro;
using Simemes.Tasks;
using System.Linq;
using Simemes.Frene;

namespace Simemes.UI.Frene
{
    public class UISearchFrene : UIPanel
    {
        [SerializeField] private List<UIFreneSlot> _freneSlots;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private UIFreneSlot _source;
        [SerializeField] private Transform _parent;

        protected override void OnHidePanel()
        {
            base.OnHidePanel();
            //_inputField.onValueChanged.RemoveListener(SearchBase);
            _inputField.onSubmit.AddListener(SearchBase);
            _inputField.text = "";
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();
            //SearchBase();
            //_inputField.onValueChanged.AddListener(SearchBase);
            _inputField.onSubmit.AddListener(SearchBase);

            List<FreneData> freneDatas = UIFrenePanel.FreneDatas;
            if (_freneSlots.Count < freneDatas.Count)
            {
                int count = freneDatas.Count - _freneSlots.Count;
                for (int i = 0; i < count; ++i)
                {
                    _freneSlots.Add(Instantiate(_source, _parent));
                }
            }

            for (int i = 0; i < _freneSlots.Count; ++i)
            {
                bool active = i < freneDatas.Count;
                UIFreneSlot slot = _freneSlots[i];
                slot.gameObject.SetActive(active);
                if (active)
                    slot.Set(freneDatas[i], i);
            }
        }

        public void Search()
        {
            SearchBase(_inputField.text);
        }

        private async void SearchBase(string keyword = "")
        {
            await FreneSystem.instance.SearchFrene(keyword, result =>
            {
                for (int i = 0; i < _freneSlots.Count; ++i)
                {
                    bool active = i < result.Count;
                    UIFreneSlot slot = _freneSlots[i];
                    slot.gameObject.SetActive(active);
                    if (active)
                    {
                        slot.Set(result[i], i);
                    }
                }
            });

            //List<int> result = UIFrenePanel.FreneDatas.Where(item => item.name.StartsWith(keyword, System.StringComparison.OrdinalIgnoreCase)).Select(item => UIFrenePanel.FreneDatas.IndexOf(item)).ToList();

            //for (int i = 0; i < _freneSlots.Count; ++i)
            //{
            //    UIFreneSlot slot = _freneSlots[i];
            //    slot.gameObject.SetActive(false);
            //}

            //for (int i = 0; i < result.Count; ++i)
            //{
            //    _freneSlots[result[i]].gameObject.SetActive(true);
            //}
        }
    }
}
