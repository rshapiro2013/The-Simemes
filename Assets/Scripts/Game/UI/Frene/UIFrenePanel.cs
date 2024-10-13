using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using TMPro;
using Simemes.Tasks;
using Simemes.Frene;
using UnityEngine.UI;
using Simemes.Request;
using Simemes.Profile;
using Core.Networking;

namespace Simemes.UI.Frene
{
    public class UIFrenePanel : UIPanel
    {
        //public static UIFrenePanel Instance;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _tag;

        [SerializeField] private List<UIFreneSlot> _freneSlots;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private UIStolenView _stolenView;
        [SerializeField] private Sprite[] _images;

        [SerializeField] private WebImage _profileImage;

        private static List<FreneData> _datas = new List<FreneData>();
        private Dictionary<FreneData, PlayerData> _frensMap = new Dictionary<FreneData, PlayerData>();

        public static List<FreneData> FreneDatas => _datas;

        protected override void Awake()
        {
            //Instance = this;
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //Instance = null;
        }

        public void Visit(FreneData data)
        {
            //FreneData data = _datas[index];

            if (!_frensMap.TryGetValue(data, out PlayerData playerData))
            {
                playerData = new PlayerData() { ID = data.id, Name = 0, Titles = 0, Sprite = 0, LastUpdate = System.DateTime.Now };
                int chestCount = Random.Range(1, 8);
                for (int i = 0; i < chestCount; ++i)
                {
                    playerData.Treasures.Add(new TreasureData() { RemainTime = Random.Range(1000, 86400), HasBuff = Random.Range(0, 100) < 50 });
                }
                _frensMap[data] = playerData;
            }

            if (_stolenView != null)
            {
                int spriteIndex = 0 % _images.Length;
                _stolenView.LoadInfo(playerData, data.name, data.screenName, _images[spriteIndex]);
                _stolenView.gameObject.SetActive(true);
            }
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

            if (_name != null)
            {
                _name.text = GameManager.instance.PlayerProfile.UserName;
            }

            if (_tag != null)
            {
                _tag.text = $"@{RequestSystem.instance.FriendID}" ;
            }
            _count.text = _datas.Count.ToString();
        }

        public void SelectProfilePhoto()
        {
            Core.Utilities.ImageLoader.instance.SelectImage(OnUploadImage);
        }

        protected void OnSelectImage(Texture2D image)
        {
            var sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
            _profileImage.Image.sprite = sprite;
        }

        protected void OnSelectImage(string url)
        {
            GameManager.instance.ChangeProfileImage(url);
        }

        protected void OnUploadImage(byte[] imageBytes)
        {
            FileRequest.UploadFile("UserIcon", imageBytes, OnSelectImage);
        }

        protected override void OnHidePanel()
        {
            base.OnHidePanel();
            if (_stolenView != null)
                _stolenView.gameObject.SetActive(false);
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            // 沒有通知資料，隨機產生
            if (_datas.Count == 0)
            {
                //for (int i = 0; i < 8; ++i)
                //{
                //    var taskData = new FreneData() { name = UIStolenInfo.Names[i], coinAmount = Random.Range(50, 1000000000) };
                //    _datas.Add(taskData);
                //}
                //_datas.Sort((x, y) => y.coinAmount.CompareTo(x.coinAmount));

                _datas = FreneSystem.instance.Datas;
            }

            Open();
        }
    }
}
