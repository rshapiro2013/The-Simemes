using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Core.Networking;
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
                PlayerProfile.Character = "yellow pepe farmer";

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

        private void OnGUI()
        {
            if (GUILayout.Button("Clear Player Data"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}
