using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Simemes.Treasures;

namespace Simemes.Request
{
    public class TreasureRequest
    {
        public delegate void ResponseMethod(List<Treasures.ChestData> treasureList);

        private static Dictionary<string, object> _requestData = new Dictionary<string, object>();

        public static event System.Action<List<ChestData>> OnUpdateList;

        public static async Task AddTreasureBox(int slotIdx, int boxID)
        {
            _requestData.Clear();
            _requestData["slotId"] = slotIdx;
            _requestData["chestDataId"] = boxID;

            await RequestSystem.instance.Post("api/PlayerChest", _requestData, OnReceive);
        }

        public static async Task CloseTreasureBox(int slotIdx)
        {
            _requestData.Clear();
            _requestData["slotId"] = slotIdx;
            _requestData["sealChestBox"] = true;

            await RequestSystem.instance.Post("api/PlayerChestUtil", _requestData, OnReceive);
        }

        public static async Task AddTreasureItem(int slotIdx, int itemID)
        {
            _requestData.Clear();
            _requestData["slotId"] = slotIdx;
            _requestData["itemId"] = itemID;

            await RequestSystem.instance.Post("api/PlayerChestUtil", _requestData, OnReceive);
        }

        public static async Task Enchant(int slotIdx, int buffID)
        {
            _requestData.Clear();
            _requestData["slotId"] = slotIdx;
            _requestData["buffId"] = buffID;

            await RequestSystem.instance.Post("api/PlayerChestUtil", _requestData, OnReceive);
        }

        public static async Task GetTreasures()
        {
            await RequestSystem.instance.Get("api/PlayerChest", OnReceive);
        }

        private static void OnReceive(string text, bool isError)
        {
            var response = JsonConvert.DeserializeObject<JSON>(text);
            var list = response.ParseList<Treasures.ChestData>("chestBoxes");
            OnUpdateList?.Invoke(list);

        }
    }
}
