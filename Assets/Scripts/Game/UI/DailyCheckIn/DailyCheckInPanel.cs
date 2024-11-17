using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using TMPro;

namespace Simemes.UI
{
    public class DailyCheckInPanel : UIPanel
    {
        [SerializeField]
        private List<RewardSlot> _rewardSlots;

        [SerializeField]
        private RewardSlot _todayReward;

        [SerializeField]
        private TextMeshProUGUI _dailyText;

        private Rewards.RewardData _receivedReward;

        protected override void Awake()
        {
            base.Awake();

            DailyCheckInSystem.instance.OnCheckIn += ShowCheckIn;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (DailyCheckInSystem.instanceExists)
                DailyCheckInSystem.instance.OnCheckIn -= ShowCheckIn;
        }

        private void ShowCheckIn(int days)
        {
            _dailyText.text = $"Day {days}";

            var dailyCheckInSys = DailyCheckInSystem.instance;

            for (int i=0;i<_rewardSlots.Count;++i)
            {
                var reward = dailyCheckInSys.GetReward(i);
                _rewardSlots[i].Set(reward);
                if (i <= days - 1)
                    _rewardSlots[i].SetReceived();
            }

            _receivedReward = dailyCheckInSys.GetReward(days - 1);
            _todayReward.Set(_receivedReward);

            EnablePanel(true);
        }

        public void Share()
        {
            var reward = Rewards.RewardMgr.instance.GetRewardConfig(_receivedReward.ID);
            string msg = $"I just claimed {_receivedReward.Count} {reward.Name}, meet me in SIMemes.";
            TelegramController.instance.Share(msg);
        }

        public void CopyShareLink()
        {
            TelegramController.instance.CopyShareLink();
        }
    }
}
