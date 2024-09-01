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

        [SerializeField] private List<UINotificationSlot> _notificationSlots;

        private readonly Dictionary<NotificationType, List<NotificationData>> _notificationData = new Dictionary<NotificationType, List<NotificationData>>();

        public void Open(int notificationType)
        {
            Open((NotificationType)notificationType);
        }

        public void Open(NotificationType notificationType)
        {
            if (_notificationData.TryGetValue(notificationType, out List<NotificationData> notificationSlots))
            {
                // 初始化通知欄位
                for (int i = 0; i < _notificationSlots.Count; ++i)
                {
                    bool active = i < notificationSlots.Count;
                    UINotificationSlot notificationSlot = _notificationSlots[i];
                    notificationSlot.gameObject.SetActive(active);
                    if (active)
                        notificationSlot.Set(notificationSlots[i]);
                }
            }
        }

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            // 沒有通知資料，隨機產生
            if (_notificationData.Count == 0)
            {
                for (int i = 0; i < (int)NotificationType.Count; ++i)
                {
                    List<NotificationData> list = new List<NotificationData>(); 
                    int count = i == 0 ? _notificationSlots.Count : 5;
                    for (int j = 0; j < count; ++j)
                    {
                        var taskData = new NotificationData() { Title = "Follow Blum's CMO on X" };
                        list.Add(taskData);
                    }
                    _notificationData[(NotificationType)i]= list;
                }
            }

            Open(NotificationType.Announcement);
        }
    }
}
