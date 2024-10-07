using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Tasks;

namespace Simemes.Frene
{
    public class FreneData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string screenName { get; set; }
        public string profileImageUrl { get; set; }
        public int coinAmount { get; set; }
        public List<string> items { get; set; }
    }
}