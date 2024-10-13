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

            _todayReward.Set(dailyCheckInSys.GetReward(days - 1));

            EnablePanel(true);
        }
    }
}
