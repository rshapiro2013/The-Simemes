using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleSignIn.Telegram.Scripts;

namespace Core.Auth
{
    public class FakeSignIn : AuthMethod
    {
        [SerializeField]
        private string _id = "fakeID";
        public override string MethodName => "FakeSignIn";

        public override void Init() => SignIn();

        public override string UserID => SystemInfo.deviceUniqueIdentifier + "_" + System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public override void SignIn()
        {
            _isSignedIn = true;

            _authInfo = new AuthInfo();
            _authInfo.Id = _id;

            _authInfo.Username = "FakeAuth";
            //if (string.IsNullOrEmpty(_authInfo.Username))
            //    _authInfo.Username = userInfo.FirstName + " " + userInfo.LastName;

            //_authInfo.PhotoUrl = userInfo.PhotoUrl;
            _authInfo.Hash = _authInfo.Username.GetHashCode().ToString();
        }
    }
}