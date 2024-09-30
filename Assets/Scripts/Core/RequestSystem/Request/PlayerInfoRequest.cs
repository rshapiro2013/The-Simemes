using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Networking
{
    public class PlayerInfoRequest<T>
    {
        public class RequestData
        {
            public string userInfo;
        }

        public class ResponseData
        {
            public string userInfo;
            public int errorCode;
            public string message;
        }

        private static T _playerInfo;

        public static async Task LoadPlayerData(T playerInfo)
        {
            _playerInfo = playerInfo;
            await RequestSystem.instance.Get("api/UserInfo", OnReceive);
        }

        public static async Task SavePlayerData(T data)
        {
            var serialized = JsonConvert.SerializeObject(data);
            var requestData = new RequestData();
            requestData.userInfo = serialized;

            await RequestSystem.instance.Post("api/UserInfo", requestData);
        }

        private static void OnReceive(string text, bool isError)
        {
            var response = JsonConvert.DeserializeObject<ResponseData>(text);

            if(response.errorCode == 2)
            {
                SavePlayerData(_playerInfo);
                return;
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            JsonConvert.PopulateObject(response.userInfo, _playerInfo, jsonSerializerSettings);
        }
    }
}