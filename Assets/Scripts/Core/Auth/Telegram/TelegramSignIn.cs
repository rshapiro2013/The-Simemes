using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Auth
{
    public class TelegramSignIn : AuthMethod
    {
        public override string MethodName => "Telegram";

        public override string UserID => _authInfo.Id.ToString();

        public override void Init()
        {
            //_telegramAuth = new TelegramAuth();
            //_telegramAuth.TryResume(OnSignIn);
        }

        public override async void SignIn()
        {
            Debug.Log("Telegram SignIn");

            var telegramAuth = TelegramController.instance;

            if (telegramAuth == null || telegramAuth.User == null)
            {
                _isError = true;
                return;
            }

            await telegramAuth.RequestPhoto();

            var userInfo = telegramAuth.User;
             _authInfo = new AuthInfo();
            _authInfo.Id = userInfo.id.ToString();

            _authInfo.Username = userInfo.username;
            if (string.IsNullOrEmpty(_authInfo.Username))
                _authInfo.Username = userInfo.first_name + " " + userInfo.last_name;

            _authInfo.PhotoUrl = userInfo.photo_url;
            _authInfo.Hash = userInfo.GetHashCode().ToString();
            _authInfo.Referred = telegramAuth.StartParam;

            _isSignedIn = true;

            Debug.Log("Telegram SignIn Success");
        }

        public override void SignOut()
        {
            _authInfo = null;
        }

    }
}