using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Simemes.Treasures;
using Simemes.Steal;
using Simemes.Frene;

namespace Simemes.Request
{
    public class FriendRequestData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string screenName { get; set; }
        public string profileImageUrl { get; set; }
    }

    public class FriendRequest
    {
        public class ResponseFriendData
        {
            public List<FreneData> friendDatas { get; set; }
            public int errorCode { get; set; }
            public string message { get; set; }
        }

        public class ResponseFriendRequestData
        {
            public List<FriendRequestData> friendRequestDatas { get; set; }
            public int errorCode { get; set; }
            public string message { get; set; }
        }

        private static Dictionary<string, object> _requestData = new Dictionary<string, object>();

        public static async Task GetFriend(System.Action<List<FreneData>> callback = null)
        {
            await RequestSystem.instance.Get("api/friend", (text, isError) =>
            {
                ResponseFriendData response = JsonConvert.DeserializeObject<ResponseFriendData>(text);
                callback?.Invoke(response.friendDatas);
#if REQUEST_LOG
                Debug.Log(text);
#endif
            });
        }

        public static async Task PostFriend(string id, System.Action<List<FreneData>> callback = null)
        {
            _requestData.Clear();
            _requestData["id"] = id;

            await RequestSystem.instance.Post("api/friend", _requestData, (text, isError) =>
            {
                ResponseFriendData response = JsonConvert.DeserializeObject<ResponseFriendData>(text);
                callback?.Invoke(response.friendDatas);
#if REQUEST_LOG
                Debug.Log(text);
#endif
            });
        }

        public static async Task GetFriendRequest(System.Action<List<FriendRequestData>> callback = null)
        {
            await RequestSystem.instance.Get("api/friendrequest", (text, isError) =>
            {
                ResponseFriendRequestData response = JsonConvert.DeserializeObject<ResponseFriendRequestData>(text);
                callback?.Invoke(response.friendRequestDatas);
#if REQUEST_LOG
                Debug.Log(text);
#endif
            });
        }

        public static async Task PostFriendRequest(string friendId, bool isRequest = false, bool isConfirmed = false, bool isRejected = false, bool isCancelled = false, System.Action<List<FriendRequestData>> callback = null)
        {
            _requestData.Clear();
            _requestData["isRequest"] = isRequest;
            _requestData["isConfirmed"] = isConfirmed;
            _requestData["isRejected"] = isRejected;
            _requestData["isReisCancelledjected"] = isCancelled;    
            _requestData["friendId"] = friendId;

            await RequestSystem.instance.Post("api/friendrequest", _requestData, (text, isError) =>
            {
                ResponseFriendRequestData response = JsonConvert.DeserializeObject<ResponseFriendRequestData>(text);
                callback?.Invoke(response.friendRequestDatas);
#if REQUEST_LOG
                Debug.Log(text);
#endif
            });
        }
    }
}
