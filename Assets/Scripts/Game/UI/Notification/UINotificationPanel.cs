using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Simemes.Tasks;

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
        private readonly Dictionary<NotificationType, List<NotificationData>> _activitiesDataMAp = new Dictionary<NotificationType, List<NotificationData>>();

        public void Open(int notificationType)
        {
            Open((NotificationType)notificationType);
        }

        public void Open(NotificationType notificationType)
        {
            LoadData(_announcementDataMap, _announcementSlots);
            LoadData(_activitiesDataMAp, _activitiesSlots, true);

            void LoadData(Dictionary<NotificationType, List<NotificationData>> notificationDataMap, List<UINotificationSlot> notificationSlots, bool showButtons = false)
            {
                if (notificationDataMap.TryGetValue(notificationType, out List<NotificationData> notificationDataList))
                {
                    // 初始化通知欄位
                    for (int i = 0; i < notificationSlots.Count; ++i)
                    {
                        bool active = i < notificationSlots.Count;
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

            // 沒有通知資料，隨機產生
            if (_announcementDataMap.Count == 0)
            {
                for (int i = 0; i < (int)NotificationType.Count; ++i)
                {
                    List<NotificationData> list = new List<NotificationData>(); 
                    int count = i == 0 ? _announcementSlots.Count : 5;
                    for (int j = 0; j < count; ++j)
                    {
                        var taskData = new NotificationData() { Title = "Follow Blum's CMO on X" };
                        list.Add(taskData);
                    }
                    _announcementDataMap[(NotificationType)i]= list;
                }
            }

            if (_activitiesDataMAp.Count == 0)
            {
                for (int i = 0; i < (int)NotificationType.Count; ++i)
                {
                    List<NotificationData> list = new List<NotificationData>();
                    int count = i == 0 ? _activitiesSlots.Count : 5;
                    for (int j = 0; j < count; ++j)
                    {
                        var taskData = new NotificationData() { Title = "Ding sent you a frens request" };
                        list.Add(taskData);
                    }
                    _activitiesDataMAp[(NotificationType)i] = list;
                }
            }

            Open(NotificationType.Announcement);
        }
    }
}
