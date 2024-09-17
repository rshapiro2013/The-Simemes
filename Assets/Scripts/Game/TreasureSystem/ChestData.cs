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

        [JsonProperty("startTime")]
        public long StartTime;

        [JsonProperty("coolDown")]
        public int CoolDown;

        [JsonProperty("buffId")]
        public int BuffID;
    }
}
