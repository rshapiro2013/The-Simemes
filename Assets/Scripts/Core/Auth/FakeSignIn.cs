using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleSignIn.Telegram.Scripts;

namespace Core.Auth
{
    public class FakeSignIn : AuthMethod
    {
        public override string MethodName => "FakeSignIn";

        public override void Init() => SignIn();

        public override void SignIn()
        {
            _isSignedIn = true;

            _authInfo = new AuthInfo();
            _authInfo.Id = 1234567;

            _authInfo.Username = "FakeAuth";
            //if (string.IsNullOrEmpty(_authInfo.Username))
            //    _authInfo.Username = userInfo.FirstName + " " + userInfo.LastName;

            //_authInfo.PhotoUrl = userInfo.PhotoUrl;
            _authInfo.Hash = _authInfo.Username.GetHashCode().ToString();
        }
    }
}