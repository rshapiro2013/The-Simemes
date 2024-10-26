using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Rewards
{
    [CreateAssetMenu(fileName = "Pebble", menuName = "Simemes/Reward/Pebble")]
    public class Reward_Pebble : RewardConfig
    {
        public override void Obtain(int count = 1)
        {
            base.Obtain(count);

            GameManager.instance.PlayerProfile.AddDiamond(count);
        }

        public override bool Check(int count)
        {
            return GameManager.instance.PlayerProfile.CheckDiamond(count);
        }
    }
}
