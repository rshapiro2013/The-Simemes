using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using TMPro;

namespace Simemes.UI
{
    public class UIInvitePanel : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI _referralText;

        protected override void OnShowPanel()
        {
            base.OnShowPanel();

            _referralText.text = TelegramController.instance.GetReferralLink();
        }

        public void Share()
        {
            TelegramController.instance.Share("Join me on SIMemes!");
        }

        public void CopyShareLink()
        {
            TelegramController.instance.CopyShareLink();
        }
    }
}