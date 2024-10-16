using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Core.Networking;
using Newtonsoft.Json;

namespace Core.Auth
{
    public class AuthMgr : MonoSingleton<AuthMgr>
    {
        [SerializeField]
        private int _authMethodIndex = 0;
        [SerializeField]
        private List<AuthMethod> _authMethods;

        private readonly Dictionary<string, AuthMethod> _methodDict = new Dictionary<string, AuthMethod>();

        private AuthMethod _authMethod;

        public AuthInfo UserInfo => _authMethod != null ? _authMethod.AuthInfo : null;

        protected override void Awake()
        {
            base.Awake();

            foreach (var method in _authMethods)
            {
                method.Init();
                _methodDict[method.MethodName] = method;
            }
        }

        public async Task SignIn()
        {
            await SignIn(_authMethods[_authMethodIndex].MethodName);
        }

        public async Task SignIn(string methodName)
        {
            _methodDict.TryGetValue(methodName, out _authMethod);
            _authMethod.SignIn();

            while (!_authMethod.IsSignedIn && !_authMethod.IsError)
                await UniTask.DelayFrame(20);

            if (_authMethod.IsError)
                return;

            await Login();
        }

        public void SignOut()
        {
            _authMethod.SignOut();
        }

        private async Task Login()
        {
            string playerID = PlayerPrefs.GetString("PlayerID", string.Empty);
            if (string.IsNullOrEmpty(playerID))
            {
                playerID = _authMethod.UserID;
                PlayerPrefs.SetString("PlayerID", playerID);
            }

            Dictionary<string, object> args = new Dictionary<string, object>();
            string name = _authMethod.AuthInfo.Username;

            args["id"] = playerID;
            if (!string.IsNullOrEmpty(name))
                args["name"] = name;

            if (!string.IsNullOrEmpty(_authMethod.AuthInfo.PhotoUrl))
                args["profileImageUrl"] = _authMethod.AuthInfo.PhotoUrl;
            
            await RequestSystem.instance.Post("api/login", args, OnSignedIn);
        }

        private void OnSignedIn(string text, bool isError)
        {
            var response = JsonConvert.DeserializeObject<ResponseData>(text);
            if (response.errorCode != 0)
                return;

            RequestSystem.instance.AuthCode = (string)response.data["authCode"];
            RequestSystem.instance.FriendID = (string)response.data["friendId"];
        }
    }
}
