using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Simemes.Profile;

namespace Simemes
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public PlayerProfile PlayerProfile = new PlayerProfile();

        private void Start()
        {
            RequestSystem.instance.RequestData("PlayerProfile", PlayerProfile);

            if (string.IsNullOrEmpty(PlayerProfile.Character))
            {
                PlayerProfile.Character = "yellow pepe farmer";

                RequestSystem.instance.UploadData("PlayerProfile", PlayerProfile);
            }

            PlayerProfile.Init();
        }
    }
}
