using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simemes.Tasks;

namespace Simemes.UI.Notification
{
    public class UINotificationSlot : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private NotificationData _notification;

        public NotificationData Notification => _notification;


        public void Set(NotificationData data)
        {
            _notification = data;

            //_icon.sprite = data.Icon;
            _title.text = data.Title;
        }
    }
}