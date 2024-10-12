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
        public PlayerProfile PlayerProfile = new PlayerProfile();

        public async Task LoadPlayerData()
        {
            RequestSystem.instance.RequestData("PlayerProfile", PlayerProfile);
            if (string.IsNullOrEmpty(PlayerProfile.Character))
            {
                InitPlayerProfile();

                RequestSystem.instance.UploadData("PlayerProfile", PlayerProfile);

                await PlayerInfoRequest<PlayerProfile>.SavePlayerData(PlayerProfile);
            }
            else
                await PlayerInfoRequest<PlayerProfile>.LoadPlayerData(PlayerProfile);

            PlayerProfile.Init();

        }

        public async Task SavePlayerData()
        {
            RequestSystem.instance.UploadData("PlayerProfile", PlayerProfile);
            await PlayerInfoRequest<PlayerProfile>.SavePlayerData(PlayerProfile);
        }

        public async Task ChangeProfileImage(string url)
        {
            PlayerProfile.PhotoUrl = url;
            PlayerProfile.Update();

            await SavePlayerData();
        }

        private void InitPlayerProfile()
        {
            PlayerProfile.Character = "yellow pepe farmer";

            var userInfo = AuthMgr.instance.UserInfo;
            if (userInfo != null)
            {
                PlayerProfile.UserName = userInfo.Username;
                PlayerProfile.PhotoUrl = userInfo.PhotoUrl;
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Clear Player Data"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}
