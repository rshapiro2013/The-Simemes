using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core.UI;

namespace Simemes.UI
{
    public class UIStorePanel : UIPanel
    {
        [Header("Events")]
        [SerializeField]
        private UnityEvent _onPurchaseItem;

        public void PurchaseItem()
        {
            _onPurchaseItem.Invoke();

            EnablePanel(false);
        }
    }
}