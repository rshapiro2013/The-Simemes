using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Simemes.Treasures;
using Simemes.Request;
using Simemes.Steal;


namespace Simemes.UI
{
    public class TreasureData
    {
        public float RemainTime;
        public bool HasBuff;
    }

    public class PlayerData
    {
        public string ID;
        public int Sprite;
        public int Name;
        public int Titles;
        public System.DateTime LastUpdate;
        public List<TreasureData> Treasures = new List<TreasureData>();
    }

    public class UIStolenInfo : MonoBehaviour
    {
        [SerializeField] private UIChestPanel _chestPanel;
        [SerializeField] private Text _name;
        [SerializeField] private Text _title;
        [SerializeField] private Image _photo;
        [SerializeField] private List<UIChestSlot> _slots;


        [SerializeField] private Button _stealBtn;
        [SerializeField] private GameObject _popupFrame;
        [SerializeField] private Text _popupText;
        [SerializeField] private UIStolenView _stolenView;

        // for local Test ======================================================
        [SerializeField] private Sprite[] _images;
        public static string[] Names = {
            "Calvina",
            "Kensey",
            "Martelli",
            "Neo",
            "Sonel",
            "Behramji",
            "Dashiell",
            "Mikala",
            "Siofra",
            "Kamiyah",
            "Cissy",
            "Barossa",
            "Spirit",
            "Barbra",
            "Takashia",
            "Rihanna",
            "Ainhoa",
            "Telemachus",
            "Vanna",
            "Derry",
        };

        private string[] _titles = {
            "Farmer",
            "Worker",
            "Clerk",
            "Technician",
            "Teacher",
            "Police Officer",
            "Sales Manager",
            "Department Supervisor",
            "Factory Manager",
            "Bank Manager",
            "Local Councilor",
            "Mayor",
            "Member of Parliament",
            "Governor",
            "Cabinet Member/Minister",
            "Deputy Prime Minister/Vice President",
            "Prime Minister",
            "Presidential Assistant",
            "Vice President",
            "President"
        };


        private List<PlayerData> _playerRecord = new List<PlayerData>();
        // ===============================================================
        private PlayerData _playerData;
        string _lastName;
        string _lastTitle;
        Sprite _lastSprite;

        private int rewindCount = 1;
        private int recommendationCount = 1;
        private int _chestCount;
        private int _playerDataIndex = 0;

        private void Awake()
        {
            rewindCount = SystemSetting.Config.RewindCount;
            recommendationCount = SystemSetting.Config.RecommendationCount;
            OnUpdateChests(StealSystem.instance.ChestDatas);
        }

        private void OnEnable()
        {
            //LoadNewInfo();
        }

        private void Set(string name, string title, Sprite sprite)
        {
            _name.text = name;
            _title.text = title;
            _photo.sprite = sprite;
            _stealBtn.interactable = true;
        }

        public void Steal()
        {
            _stolenView.LoadInfo(_playerData, _lastName, _lastTitle, _lastSprite);
            _stolenView.gameObject.SetActive(true);
        }

        public void Steal(int index)
        {
            //if (_chestPanel.TryGetEmptySlot(out UIChestSlot empty))
            //{
            //    bool success;
            //    bool hasBuff;

            //    //int index = Random.Range(0, _chestCount);
                UIChestSlot slot = _slots[index];
            //    hasBuff = slot.Content.HasBuff;
            //    success = hasBuff ? false : Random.Range(0, 100) < 75;

            //    if (hasBuff)
            //    {
            //        _stealBtn.interactable = false;
            //    }

            //    if (success)
            //    {
                    slot.gameObject.SetActive(false);

            //        // remove treasure data
            //        PlayerData playerData = _playerRecord[_playerDataIndex];
            //        playerData.Treasures.RemoveAt(index);

            //        if (_chestPanel != null)
            //        {
            //            _chestPanel.AddChest(_chestPanel.Slots.IndexOf(empty));

            //            empty.SetBox(slot.Content);
            //            empty.Seal();
            //        }
            //    }
            //    _popupText.text = hasBuff ? "Trigger guard!\n<color=red>Steal failed...</color>" : success ? "Steal succeeded!" : "<color=red>Steal failed...</color>"; ;// "偷取失敗\n觸發防護罩" : success ? "成功偷取" : "偷取失敗";
            //}
            //else
            //{
            //    _popupText.text = "Your chest is full";//"你的寶箱已滿";
            //}

            //_popupFrame.SetActive(true);


            _playerRecord.Remove(_playerData);
            //_playerDataIndex--;
            LoadNextInfo();
        }

        public void Ban()
        {
            if (!_playerRecord.Contains(_playerData))
            {
                _playerRecord.Add(_playerData);
                _playerDataIndex = _playerRecord.Count - 1;
            }
            LoadNextInfo();
        }

        public void Popup(string msg)
        {
            _popupText.text = msg;
            _popupFrame.SetActive(true);
        }

        public void LoadNextInfo()
        {
            //if (_playerDataIndex < _playerRecord.Count - 1)
            //    LoadInfoBase(++_playerDataIndex);
            //else
            if(recommendationCount < 1)
            {
                Popup("No recommendation count");
                return;
            }

            int playerDataIndex = _playerDataIndex + 1;
            if (playerDataIndex < _playerRecord.Count)
            {
                --recommendationCount;
                _playerDataIndex = playerDataIndex;
                LoadInfoBase(_playerDataIndex);
            }
            //LoadNewInfo();
        }

        public void LoadPreviousInfo()
        {
            if (rewindCount < 1)
            {
                Popup("No rewind count");
                return;
            }

            int playerDataIndex = _playerDataIndex - 1;
            if (playerDataIndex > -1)
            {
                --rewindCount;
                _playerDataIndex = playerDataIndex;
                LoadInfoBase(_playerDataIndex);
            }
        }

        private void LoadChestData(ChestDatas chestDatas)
        {
            int nameIndex = Random.Range(0, Names.Length);
            _lastName = Names[nameIndex];
            _lastTitle = _titles[nameIndex];
            int spriteIndex = nameIndex % _images.Length;// < _images.Length ? nameIndex : _images.Length - 1;
            _lastSprite = _images[spriteIndex];

            int chestCount = chestDatas.ChestDataList.Count;
            for (int i = 0; i < _slots.Count; ++i)
            {
                bool enable = i < chestCount;
                UIChestSlot slot = _slots[i];
                slot.gameObject.SetActive(enable);
                if (enable)
                {
                    var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(21011);
                    if (treasureBoxConfig == null)
                        return;

                    var treasureBox = new TreasureBox(treasureBoxConfig);
                    treasureBox.RemainTime = Random.Range(1000, 86400);

                    slot.SetBox(treasureBox);

                    if (Random.Range(0, 100) < 50)
                        slot.AddBuff(TreasureSystem.instance.GetBuff(5001));

                    slot.Seal();
                }
            }

            _playerData = new PlayerData() { ID = chestDatas.UserID, Name = nameIndex, Titles = nameIndex, Sprite = spriteIndex, LastUpdate = System.DateTime.Now };
            for (int i = 0; i < chestCount; ++i)
            {
                UIChestSlot slot = _slots[i];
                _playerData.Treasures.Add(new TreasureData() { RemainTime = slot.Content.RemainTime, HasBuff = slot.Content.HasBuff });
            }
            _playerRecord.Add(_playerData);
        }

        private void LoadChestData()
        {
            _chestCount = Random.Range(1, _slots.Count);
            for (int i = 0; i < _slots.Count; ++i)
            {
                bool enable = i < _chestCount;
                UIChestSlot slot = _slots[i];
                slot.gameObject.SetActive(enable);
                if (enable)
                {
                    var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(21011);
                    if (treasureBoxConfig == null)
                        return;

                    var treasureBox = new TreasureBox(treasureBoxConfig);
                    treasureBox.RemainTime = Random.Range(1000, 86400);

                    slot.SetBox(treasureBox);

                    if (Random.Range(0, 100) < 50)
                        slot.AddBuff(TreasureSystem.instance.GetBuff(5001));

                    slot.Seal();
                }
            }
        }

        private void LoadChestData(PlayerData playerData)
        {
            System.DateTime now = System.DateTime.Now;
            _chestCount = playerData.Treasures.Count;
            for (int i = 0; i < _slots.Count; ++i)
            {
                bool enable = i < _chestCount;
                UIChestSlot slot = _slots[i];
                slot.gameObject.SetActive(enable);
                if (enable)
                {
                    var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(21011);
                    if (treasureBoxConfig == null)
                        return;

                    TreasureData treasureData = playerData.Treasures[i];
                    var treasureBox = new TreasureBox(treasureBoxConfig);
                    treasureBox.RemainTime = (float)(treasureData.RemainTime - (now - playerData.LastUpdate).TotalSeconds);

                    slot.SetBox(treasureBox);

                    if (treasureData.HasBuff)
                        slot.AddBuff(TreasureSystem.instance.GetBuff(5001));

                    slot.Seal();
                }
            }
        }

        //private void LoadNewInfo()
        //{
        //    // for local Test ======================================================
        //    int nameIndex = Random.Range(0, Names.Length);
        //    _lastName = Names[nameIndex];
        //    _lastTitle = _titles[nameIndex];
        //    int spriteIndex = nameIndex % _images.Length;// < _images.Length ? nameIndex : _images.Length - 1;
        //    _lastSprite = _images[spriteIndex];
        //    LoadChestData();

        //    // log player data
        //    _playerData = new PlayerData() { Name = nameIndex, Titles = nameIndex, Sprite = spriteIndex, LastUpdate = System.DateTime.Now };
        //    for (int i = 0; i < _chestCount; ++i)
        //    {
        //        UIChestSlot slot = _slots[i];
        //        _playerData.Treasures.Add(new TreasureData() { RemainTime = slot.Content.RemainTime, HasBuff = slot.Content.HasBuff });
        //    }
        //    //_playerRecord.Add(_playerData);
        //    //_playerDataIndex = _playerRecord.Count - 1;

        //    // ===============================================================

        //    Set(_lastName, _lastTitle, _lastSprite);
        //}

        private void LoadInfoBase(int index)
        {
            _playerData = _playerRecord[index];
            _lastName = Names[_playerData.Name];
            _lastTitle = _titles[_playerData.Titles];
            _lastSprite = _images[_playerData.Sprite];
            LoadChestData(_playerData);
            Set(_lastName, _lastTitle, _lastSprite);
        }

        private void OnUpdateChests(List<ChestDatas> ChestDatas)
        {
            GameFlow.MainThread.Post(_ =>
            {
                foreach (ChestDatas chest in ChestDatas)
                {
                    LoadChestData(chest);
                }
                LoadInfoBase(_playerDataIndex);
            }, null);
        }
    }
}