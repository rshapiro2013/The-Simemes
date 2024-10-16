using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleSignIn.Telegram.Scripts;

namespace Core.Auth
{
    public class TelegramSignIn : AuthMethod
    {
        // Telegram Data
        private TelegramAuth _telegramAuth;
        private UserInfo _userInfo;

        public override string MethodName => "Telegram";

        public override string UserID => _authInfo.Id.ToString();

        public override void Init()
        {
            _telegramAuth = new TelegramAuth();
            _telegramAuth.TryResume(OnSignIn);
        }

        public override void SignIn()
        {
            _telegramAuth.SignIn(OnSignIn, caching: true);
        }

        public override void SignOut()
        {
            _telegramAuth.SignOut();
            _authInfo = null;
        }

        private void OnSignIn(bool success, string error, UserInfo userInfo)
        {
            _isSignedIn = success;
            _userInfo = userInfo;

            _authInfo = new AuthInfo();
            _authInfo.Id = userInfo.Id.ToString();

            _authInfo.Username = userInfo.Username;
            if (string.IsNullOrEmpty(_authInfo.Username))
                _authInfo.Username = userInfo.FirstName + " " + userInfo.LastName;

            _authInfo.PhotoUrl = userInfo.PhotoUrl;
            _authInfo.Hash = userInfo.Hash;
        }
    }
}