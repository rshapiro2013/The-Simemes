using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Simemes.Treasures;
using Simemes.Steal;

namespace Simemes.Request
{
    /*
     {
        "screenName": "",
        "profileImageUrl": "",
        "coinAmount": 104
     }
    */
    [System.Serializable]
    public class RankData
    {
        string screenName;
        string profileImageUrl;
        public int coinAmount;
    }

    public class RankRequest
    {
        /*
         {
            "userRank": 11,
            "rankDatas": [],
            "errorCode": 0,
            "message": ""
         }
        */
        public class ResponseData
        {
            public int userRank;
            public List<RankData> rankDatas;
            public int errorCode;
            public string message;
        }

        public static event System.Action<int, List<RankData>> OnUpdateRank;

        public static async Task Rank(System.Action<ResponseData> callback = null)
        {
            await RequestSystem.instance.Get("api/rank", (text, isError) =>
            {
                ResponseData response = JsonConvert.DeserializeObject<ResponseData>(text);
                callback?.Invoke(response);

#if REQUEST_LOG
                Debug.Log(text);
#endif
            });
        }
    }
}
