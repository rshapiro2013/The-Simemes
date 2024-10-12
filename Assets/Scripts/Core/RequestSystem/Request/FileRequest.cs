using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simemes.Request
{
    public class FileRequest
    {
        private static System.Action<string> _callback;
        public static async Task UploadFile(string type, byte[] data, System.Action<string> callback = null)
        {
            _callback = callback;

            var form = new WWWForm();
            form.AddField("FileAttr", type);
            form.AddBinaryData("file", data);

            await RequestSystem.instance.Post("File", form, OnReceive);
        }

        public static async Task GetFile(string type, System.Action<string> callback = null)
        {
            await RequestSystem.instance.Get($"File?fileAttr={type}");
        }

        private static void OnReceive(string text, bool isError)
        {
            var response = JsonConvert.DeserializeObject<JSON>(text);
            if(response.TryGetValue("fileUrl", out var url))
            {
                _callback?.Invoke((string)url);
            }
        }
    }
}
