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
    public class StealRequest
    {
        public delegate void ResponseMethod(List<ChestDatas> chestDataList);

        private static Dictionary<string, object> _requestData = new Dictionary<string, object>();

        public static event System.Action<List<ChestDatas>> OnUpdateChests;

        public static async Task GetChestDatas()
        {
            await RequestSystem.instance.Get("api/StealChest", OnReceive);
        }

        public static async Task StealChest(string userGuid, int slotIdx)
        {
            _requestData.Clear();
            _requestData["slotId"] = slotIdx;
            _requestData["userGuid"] = userGuid;

            await RequestSystem.instance.Post("api/StealChest", _requestData, OnReceive);
        }

        private static void OnReceive(string text, bool isError)
        {
            var response = JsonConvert.DeserializeObject<JSON>(text);
            var list = response.ParseList<ChestDatas>("userChestDatas");
            OnUpdateChests?.Invoke(list);
        }
    }
}
