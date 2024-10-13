using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Simemes.Tasks;
using Simemes.Frene;
using Simemes.Request;

namespace Simemes.UI.Notification
{
    public class UINotificationPanel : UIPanel
    {
        public enum NotificationType
        {
            Announcement,
            Activities,
            Count
        }

        [SerializeField] private GameObject _announcement;
        [SerializeField] private GameObject _activities;

        [SerializeField] private List<UINotificationSlot> _announcementSlots;
        [SerializeField] private List<UINotificationSlot> _activitiesSlots;

        private readonly Dictionary<NotificationType, List<NotificationData>> _announcementDataMap = new Dictionary<NotificationType, List<NotificationData>>();
        //private readonly Dictionary<NotificationType, List<NotificationData>> _activitiesDataMap = new Dictionary<NotificationType, List<NotificationData>>();

        public void Open(int notificationType)
        {
            Open((NotificationType)notificationType);
        }

        public void Open(NotificationType notificationType)
        {
            if(notificationType == NotificationType.Announcement)
                LoadData(_announcementDataMap, _announcementSlots);
            else
                LoadData(_announcementDataMap, _activitiesSlots, true);

            void LoadData(Dictionary<NotificationType, List<NotificationData>> notificationDataMap, List<UINotificationSlot> notificationSlots, bool showButtons = false)
            {
                if (notificationDataMap.TryGetValue(notificationType, out List<NotificationData> notificationDataList))
                {
                    // 初始化通知欄位
                    for (int i = 0; i < notificationSlots.Count; ++i)
                    {
                        bool active = i < notificationDataList.Count;
                        UINotificationSlot notificationSlot = notificationSlots[i];
                        notificationSlot.gameObject.SetActive(active);
                        if (active)
                            notificationSlot.Set(notificationDataList[i]);
                        notificationSlot.ShowButtons(showButtons);
                    }
                }
            }
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            // Activities
            if (!_announcementDataMap.TryGetValue(NotificationType.Activities, out List<NotificationData> dataList))
            {
                dataList = new List<NotificationData>();
                _announcementDataMap[NotificationType.Activities] = dataList;
            }
            else
                dataList.Clear();

            List<FriendRequestData> freneRequestDatas = FreneSystem.instance.FreneRequestDatas;
            for (int i = 0; i < freneRequestDatas.Count; ++i)
            {
                FriendRequestData data = freneRequestDatas[i];
                var taskData = new NotificationData() { Title = $"{data.name} sent you a frens request", ID = data.friendId };
                dataList.Add(taskData);
            }

            // 沒有通知資料，隨機產生
            if (!_announcementDataMap.ContainsKey(NotificationType.Announcement))
            {
                dataList = new List<NotificationData>();
                int count = _announcementSlots.Count;// i == 0 ? _announcementSlots.Count : 5;
                for (int j = 0; j < count; ++j)
                {
                    var taskData = new NotificationData() { Title = "Follow Blum's CMO on X" };
                    dataList.Add(taskData);
                }
                _announcementDataMap[NotificationType.Announcement]= dataList;
            }

            Open(NotificationType.Announcement);
        }
    }
}
