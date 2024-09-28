using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Rewards
{
    [CreateAssetMenu(fileName = "Coin", menuName = "Simemes/Reward/Coin")]
    public class Reward_Coin : RewardConfig
    {
        public override void Obtain(int count = 1)
        {
            base.Obtain(count);

            GameManager.instance.PlayerProfile.AddCoin(count);
        }
    }
}
