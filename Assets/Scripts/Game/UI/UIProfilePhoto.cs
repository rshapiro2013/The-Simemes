using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simemes.Profile;
using UnityEngine.UI;
using Core.UI;

namespace Simemes.UI
{
    public class UIProfilePhoto : MonoBehaviour
    {
        [SerializeField]
        private WebImage _image;

        private void Awake()
        {
            if (GameManager.instance)
            {
                var playerProfile = GameManager.instance.PlayerProfile;

                playerProfile.OnUpdateProfile += UpdatePhoto;

                UpdatePhoto(playerProfile);

            }
        }

        private void OnDestroy()
        {
            if (GameManager.instance)
            {
                var playerProfile = GameManager.instance.PlayerProfile;

                playerProfile.OnUpdateProfile -= UpdatePhoto;
            }
            
        }

        private void UpdatePhoto(PlayerProfile profile)
        {
            _image.Load(profile.PhotoUrl);
        }
    }
}
