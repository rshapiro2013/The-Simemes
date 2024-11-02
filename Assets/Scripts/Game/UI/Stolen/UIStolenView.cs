using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Simemes.Treasures;
using Simemes.Frene;
#if UNITY_EDITOR
using UnityEditor;

namespace Simemes.UI
{
    [CustomEditor(typeof(UIStolenView))]
    public class UIStolenViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UIStolenView stolenView = target as UIStolenView;
            if (GUILayout.Button("InvokeSuccess"))
            {
                stolenView.InvokeSuccess(null);
            }
            if (GUILayout.Button("InvokeFailed"))
            {
                stolenView.InvokeFailed();
            }
            base.OnInspectorGUI();
        }
    }
#else
namespace Simemes.UI
{
#endif
    public class UIStolenView : MonoBehaviour
    {
        public static string FreneID;
        [SerializeField] private UIChestPanel _chestPanel;
        [SerializeField] private Text _name;
        [SerializeField] private Text _title;
        [SerializeField] private List<UIChestSlot> _slots;

        [SerializeField] private UIStolenInfo _stolenInfo;
        [SerializeField] private Button _stealBtn;
        [SerializeField] private Button _addBtn;
        [SerializeField] private GameObject _popupFrame;
        [SerializeField] private Text _popupText;
        [SerializeField] private Image _character;
        [SerializeField] private UIBackground _uiBackground;
        [SerializeField] private UnityEvent<Sprite> _onStealSusscee;
        [SerializeField] private UnityEvent _onStealFailed;

        private int _chestCount;
        private PlayerData _playerData;


#if UNITY_EDITOR
        public void InvokeSuccess(Sprite sprite)
        {
            _onStealSusscee?.Invoke(sprite);
        }

        public void InvokeFailed()
        {
            _onStealFailed?.Invoke();
        }
#endif

        private void Set(string name, string title, string bg)
        {
            _name.text = name;
            _title.text = title;
            _uiBackground.SetBackground(bg);
            _addBtn.gameObject.SetActive(!FreneSystem.instance.HasFrene(_playerData.ID));
        }

        public void Steal(int index)
        {
            //if (_chestPanel.TryGetEmptySlot(out UIChestSlot empty))
            {
                bool success;
                bool hasBuff;

                UIChestSlot slot = _slots[index];
                hasBuff = slot.Content.HasBuff;
                success = hasBuff ? false : Random.Range(0, 100) < 50;

                //if (hasBuff)
                //    _stealBtn.interactable = false;

                if (success)
                {
                    slot.gameObject.SetActive(false);

                    // remove treasure data
                    _playerData.ChestDataList.RemoveAt(index);

                    if (_chestPanel != null)
                    {
                        _chestPanel.SetNewChest(slot.Content);
                        //_chestPanel.AddChest(_chestPanel.Slots.IndexOf(empty));

                        //empty.SetBox(slot.Content);
                        //empty.Seal();
                    }
                    _stolenInfo.Steal(index);
                    Sprite sprite = slot.Content.Items.Count > 0 ? slot.Content.Items[0].Image : null;
                    _onStealSusscee?.Invoke(sprite);
                    --SystemSetting.Config.StealCount;
                }
                else
                    _onStealFailed?.Invoke();
                if(_popupText!=null)
                    _popupText.text = hasBuff ? "Trigger guard!\n<color=red>Steal failed...</color>" : success ? "Steal succeeded!" : "<color=red>Steal failed...</color>"; ;// "偷取失敗\n觸發防護罩" : success ? "成功偷取" : "偷取失敗";
            }
            //else
            //{
            //    _popupText.text = "Your chest is full";//"你的寶箱已滿";
            //}

            if(_popupFrame!=null)
                _popupFrame.SetActive(true);
        }

        public void AddFriend()
        {
            if(!string.IsNullOrEmpty(FreneID))
                FreneSystem.instance.AddFrene(FreneID);
        }

        public void LoadInfo(PlayerData playerData, string name, string title, Sprite sprite)
        {
            LoadInfoBase(playerData, name, title, sprite);
        }

        //private void LoadChestData()
        //{
        //    _chestCount = Random.Range(1, _slots.Count);
        //    for (int i = 0; i < _slots.Count; ++i)
        //    {
        //        bool enable = i < _chestCount;
        //        UIChestSlot slot = _slots[i];
        //        slot.gameObject.SetActive(enable);
        //        if (enable)
        //        {
        //            var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(0);
        //            if (treasureBoxConfig == null)
        //                return;

        //            var treasureBox = new TreasureBox(treasureBoxConfig);
        //            treasureBox.RemainTime = Random.Range(1000, 86400);

        //            slot.SetBox(treasureBox);

        //            if (Random.Range(0, 100) < 50)
        //                slot.AddBuff(TreasureSystem.instance.GetBuff(1));

        //            slot.Seal();
        //        }
        //    }
        //}

        private void LoadChestData(PlayerData playerData)
        {
            _playerData = playerData;
            System.DateTime now = System.DateTime.Now;
            _chestCount = playerData.ChestDataList.Count;
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

                    ChestData chestData = playerData.ChestDataList[i];
                    var treasureBox = new TreasureBox(treasureBoxConfig);
                    treasureBox.Set(chestData);
                    slot.SetBox(treasureBox);
                    slot.Seal();
                }
            }
        }

        private void LoadInfoBase(PlayerData playerData, string name, string title, Sprite sprite)
        {
            LoadChestData(playerData);
            Set(name, title, playerData.Background);
            _character.sprite = sprite;
        }
    }
}