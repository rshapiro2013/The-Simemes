using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Simemes.Profile.PlayerProfile PlayerProfile = new Profile.PlayerProfile();

        private void Start()
        {
            PlayerProfile.Init();
        }
    }
}
