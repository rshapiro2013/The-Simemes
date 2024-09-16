using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using TMPro;
using Simemes.Tasks;

namespace Simemes.UI.Frene
{
    public class UIFrenePanel : UIPanel
    {
        public static UIFrenePanel Instance;

        [SerializeField] private List<UIFreneSlot> _freneSlots;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private UIStolenView _stolenView;
        [SerializeField] private Sprite[] _images;

        private static List<FreneData> _datas = new List<FreneData>();
        private Dictionary<int, PlayerData> _frensMap = new Dictionary<int, PlayerData>();

        public static List<FreneData> FreneDatas => _datas;

        protected override void Awake()
        {
            Instance = this;
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Instance = null;
        }

        public void Visit(int index)
        {
            FreneData data = _datas[index];

            if (!_frensMap.TryGetValue(index, out PlayerData playerData))
            {
                playerData = new PlayerData() { Name = index, Titles = index, Sprite = index, LastUpdate = System.DateTime.Now };
                int chestCount = Random.Range(1, 8);
                for (int i = 0; i < chestCount; ++i)
                {
                    playerData.Treasures.Add(new TreasureData() { RemainTime = Random.Range(1000, 86400), HasBuff = Random.Range(0, 100) < 50 });
                }
                _frensMap[index] = playerData;
            }

            int spriteIndex = index % _images.Length;
            _stolenView.LoadInfo(playerData, data.Name, data.Title, _images[spriteIndex]);
            _stolenView.gameObject.SetActive(true);
        }

        public void Open()
        {
            // 初始化通知欄位
            for (int i = 0; i < _freneSlots.Count; ++i)
            {
                bool active = i < _datas.Count;
                UIFreneSlot slot = _freneSlots[i];
                slot.gameObject.SetActive(active);
                if (active)
                    slot.Set(_datas[i], i);
            }

            _count.text = _datas.Count.ToString();
        }

        protected override void OnHidePanel()
        {
            base.OnHidePanel();
            _stolenView.gameObject.SetActive(false);
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            // 沒有通知資料，隨機產生
            if (_datas.Count == 0)
            {
                for (int i = 0; i < 9; ++i)
                {
                    var taskData = new FreneData() { Name = UIStolenInfo.Names[i], Count = Random.Range(50, 1000000000) };
                    _datas.Add(taskData);
                }
                _datas.Sort((x, y) => y.Count.CompareTo(x.Count));
            }

            Open();
        }
    }
}
