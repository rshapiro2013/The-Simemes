using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Simemes.Treasures
{
    [System.Serializable]
    public class ChestData
    {
        [JsonProperty("slotId")]
        public int SlotID;

        [JsonProperty("isSeal")]
        public bool IsSealed;

        [JsonProperty("chestDataId")]
        public int ChestID;

        [JsonProperty("items")]
        public List<int> Treasures;

        [JsonProperty("starttime")]
        public long StartTime;

        [JsonProperty("endtime")]
        public int EndTime;

        [JsonProperty("buffId")]
        public int BuffID;
    }
}
