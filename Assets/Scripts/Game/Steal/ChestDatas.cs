using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Simemes.Treasures;

namespace Simemes.Steal
{
    [System.Serializable]
    public class ChestDatas
    {
        [JsonProperty("userGuid")]
        public string UserID;

        [JsonProperty("chestBoxes")]
        public List<ChestData> ChestDataList;
    }
}
