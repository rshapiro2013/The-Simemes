using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System.Threading.Tasks;
using Simemes.Request;

namespace Simemes.Announcement
{
    public class AnnouncementSystem : GenericSystem<AnnouncementSystem, AnnouncementData>
    {
        public async override Task SyncData()
        {
            await AnnouncementRequest.GetAnnouncement(OnUpdateBase);
        }
    }
}