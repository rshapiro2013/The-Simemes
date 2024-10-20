using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Core.Networking;
using Core.Auth;
using Simemes.Profile;
using System.Threading.Tasks;

namespace Simemes
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public SaveData SaveData = new SaveData();

        public PlayerProfile PlayerProfile => SaveData.Profile;

        public async Task LoadPlayerData()
        {
            RequestSystem.instance.RequestData("PlayerProfile", SaveData);
            if (string.IsNullOrEmpty(SaveData.Profile.Character))
            {
                InitPlayerProfile();

                RequestSystem.instance.UploadData("PlayerProfile", SaveData);

                await PlayerInfoRequest<SaveData>.SavePlayerData(SaveData);
            }
            else
                await PlayerInfoRequest<SaveData>.LoadPlayerData(SaveData);

            SaveData.Init();

        }

        public async Task SavePlayerData()
        {
            RequestSystem.instance.UploadData("PlayerProfile", SaveData);
            await PlayerInfoRequest<SaveData>.SavePlayerData(SaveData);
        }

        public async Task ChangeProfileImage(string url)
        {
            SaveData.Profile.PhotoUrl = url;
            SaveData.Profile.Update();

            await SavePlayerData();
        }

        private void InitPlayerProfile()
        {
            SaveData.Profile.Character = "yellow pepe farmer";

            var userInfo = AuthMgr.instance.UserInfo;
            if (userInfo != null)
            {
                SaveData.Profile.UserName = userInfo.Username;
                SaveData.Profile.PhotoUrl = userInfo.PhotoUrl;
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Clear Player Data"))
            {
                PlayerPrefs.DeleteAll();
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F1))
                SaveData.Profile.AddExp(SaveData.Profile.MaxExp);

            if (Input.GetKeyUp(KeyCode.F2))
            {
                var treasureItems = Treasures.TreasureSystem.instance.TreasureItems;
                foreach (var item in treasureItems)
                {
                    if (SaveData.Collection.IsUnlocked(item.ID))
                        continue;

                    item.Obtain(1);
                    return;
                }
            }
        }
    }
}
