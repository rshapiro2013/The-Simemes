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
    public class AnnouncementData
    {
        public string announcementTitle;
        public string announcementContent;
        public string announcementBackgroundImageUrl;
        public string announcementEndTimestamp;
        public int errorCode;
        public string message;
    }

    public class AnnouncementRequest
    {
        /* 
            get body
            {
                "announcementTitle": "Test Title",
                "announcementContent": "Test A bunch of random text Test A bunch of random text Test A bunch of random text Test A bunch of random text Test A bunch of random text Test A bunch of random text ",
                "announcementBackgroundImageUrl": "www.google.com",
                "announcementEndTimestamp": 1730678543,
                "errorCode": 0,
                "message": ""
            }
        */
        public static async Task GetAnnouncement(System.Action<AnnouncementData> callback = null)
        {
            await RequestSystem.instance.Get("api/announcement", (text, isError) =>
            {
                AnnouncementData response = JsonConvert.DeserializeObject<AnnouncementData>(text);
                callback?.Invoke(response);
#if REQUEST_LOG
                Debug.Log(text);
#endif
            });
        }
    }
}
