using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Networking
{
    public class ResponseData
    {
        public Dictionary<string, object> data;
        public int errorCode;
        public string message;
    }
}