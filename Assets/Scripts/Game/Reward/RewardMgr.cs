using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes.Rewards
{
    public class RewardMgr : MonoSingleton<RewardMgr>
    {
        [SerializeField]
        private List<RewardConfig> _rewardConfig;

        public RewardConfig GetRewardConfig(int id)
        {
            var config = _rewardConfig.Find(x => x.ID == id);
            return config;
        }

        public string GetRewardText(int id, int count)
        {
            var config = GetRewardConfig(id);
            if (config == null)
                return string.Empty;

            if (!string.IsNullOrEmpty(config.RewardFormat))
                return string.Format(config.RewardFormat, config.Name, count);

            return $"{config.Name} x{count}";
        }

        public void ObtainReward(int id, int count)
        {
            var config = GetRewardConfig(id);
            if (config == null)
                return;

            config.Obtain(count);
        }
    }
}
