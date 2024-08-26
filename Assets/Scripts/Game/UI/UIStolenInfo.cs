using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Simemes.Treasures;

namespace Simemes.UI
{
    public class StolenInfo : MonoBehaviour
    {
        [SerializeField] private UIChestPanel _chestPanel;
        [SerializeField] private Text _name;
        [SerializeField] private Text _title;
        [SerializeField] private Image _photo;
        [SerializeField] private List<UIChestSlot> _slots;


        [SerializeField] private Button _stealBtn;
        [SerializeField] private GameObject _popupFrame;
        [SerializeField] private Text _popupText;

        // for local Test ======================================================
        [SerializeField] private Sprite[] _images;
        private string[] _names = {
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
        // ===============================================================

        private int _chestCount;

        private void OnEnable()
        {
            LoadInfo();
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
            if (_chestPanel.TryGetEmptySlot(out UIChestSlot empty))
            {
                bool success;
                bool hasBuff;

                UIChestSlot slot = _slots[Random.Range(0, _chestCount)];
                hasBuff = slot.Content.HasBuff;
                success = hasBuff ? false : Random.Range(0, 100) < 75;

                if (hasBuff)
                {
                    _stealBtn.interactable = false;
                }

                if (success)
                {
                    slot.gameObject.SetActive(false);
                    if (_chestPanel != null)
                    {
                        _chestPanel.AddChest(_chestPanel.Slots.IndexOf(empty));

                        empty.SetBox(slot.Content);
                        empty.Seal();
                    }
                }
                _popupText.text = hasBuff ? "Trigger guard!\n<color=red>Steal failed...</color>" : success ? "Steal succeeded!" : "<color=red>Steal failed...</color>"; ;// "偷取失敗\n觸發防護罩" : success ? "成功偷取" : "偷取失敗";
            }
            else
            {
                _popupText.text = "Your chest is full";//"你的寶箱已滿";
            }


            _popupFrame.SetActive(true);
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
                    var treasureBoxConfig = TreasureSystem.instance.GetTreasureBoxConfig(0);
                    if (treasureBoxConfig == null)
                        return;

                    var treasureBox = new TreasureBox(treasureBoxConfig);
                    treasureBox.RemainTime = Random.Range(1000, 86400);

                    slot.SetBox(treasureBox);

                    if (Random.Range(0, 100) < 50)
                        slot.AddBuff(TreasureSystem.instance.GetBuff(1));

                    slot.Seal();
                }
            }
        }

        public void LoadInfo()
        {
            // for local Test ======================================================
            int nameIndex = Random.Range(0, _names.Length);
            string name = _names[nameIndex];
            string title = _titles[nameIndex];
            Sprite sprite = _images[nameIndex < _images.Length ? nameIndex : _images.Length - 1];
            LoadChestData();
            // ===============================================================

            Set(name, title, sprite);
        }
    }
}