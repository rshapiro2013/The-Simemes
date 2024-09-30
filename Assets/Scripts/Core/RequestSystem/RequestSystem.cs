using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Core.Networking
{
    public class RequestSystem : MonoSingleton<RequestSystem>
    {
        public delegate void ResponseMethod(string data, bool isError);

        protected const string _url = "https://simemesbackendapi-gme6g6c8cwcserfw.eastasia-01.azurewebsites.net/";

        protected string _authCode;

        public async Task Post(string api, Dictionary<string, object> args, ResponseMethod callback = null)
        {
            var path = $"{_url}{api}";

            var body = GetBodyStr(args);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
            var request = new UnityWebRequest(path, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            if (!string.IsNullOrWhiteSpace(_authCode))
                request.SetRequestHeader("Authorization", _authCode);

            request.SendWebRequest();

            while (!request.isDone)
                await UniTask.DelayFrame(20); // save Power

            if (request.isHttpError || request.isNetworkError)
            {
                callback?.Invoke(request.result.ToString(), true);
            }
            else
                callback?.Invoke(request.downloadHandler.text, false);
        }

        public async Task Post<T>(string api, T data, ResponseMethod callback = null)
        {
            var path = $"{_url}{api}";

            var body = GetBodyStr<T>(data);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
            var request = new UnityWebRequest(path, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            if (!string.IsNullOrWhiteSpace(_authCode))
                request.SetRequestHeader("Authorization", _authCode);

            request.SendWebRequest();

            while (!request.isDone)
                await UniTask.DelayFrame(20); // save Power

            if (request.isHttpError || request.isNetworkError)
            {
                callback?.Invoke(request.result.ToString(), true);
            }
            else
                callback?.Invoke(request.downloadHandler.text, false);
        }

        public async Task Get(string api, ResponseMethod callback = null)
        {
            var path = $"{_url}{api}";

            var request = new UnityWebRequest(path, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            if (!string.IsNullOrWhiteSpace(_authCode))
                request.SetRequestHeader("Authorization", _authCode);

            request.SendWebRequest();

            while (!request.isDone)
                await UniTask.DelayFrame(20); // save Power

            if (request.isHttpError || request.isNetworkError)
            {
                callback?.Invoke(request.result.ToString(), true);
            }
            else
                callback?.Invoke(request.downloadHandler.text, false);
        }

        public async Task Login()
        {
            string playerID = PlayerPrefs.GetString("PlayerID", string.Empty);
            if(string.IsNullOrEmpty(playerID))
            {
                playerID = UnityEngine.Device.SystemInfo.deviceUniqueIdentifier + System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                PlayerPrefs.SetString("PlayerID", playerID);
            }    

            Dictionary<string, object> args = new Dictionary<string, object>();
            args["id"] = playerID;
            args["name"] = "Simemes User";
            await Post("api/login", args, OnSignedIn);
        }

        private void OnSignedIn(string text, bool isError)
        {
            var response = JsonConvert.DeserializeObject<ResponseData>(text);
            if(response.errorCode != 0)
            {
                HandleError(response.errorCode, response.message);
                return;
            }

            _authCode = (string)response.data["authCode"];

        }

        private void HandleError(int errorCode, string message)
        {

        }

        private static string GetBodyStr(Dictionary<string, object> args)
        {
            var str = JsonConvert.SerializeObject(args);
            return str;
        }

        private static string GetBodyStr<T>(T args)
        {
            var str = JsonConvert.SerializeObject(args);
            return str;
        }

        public void RequestData<T>(string requestName, T data)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            string serializedData = PlayerPrefs.GetString(requestName, string.Empty);
            if (!string.IsNullOrEmpty(serializedData))
                JsonConvert.PopulateObject(serializedData, data, jsonSerializerSettings);
        }

        public void UploadData<T>(string requestName, T data)
        {
            string serializedData = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(requestName, serializedData);
        }
    }
}
