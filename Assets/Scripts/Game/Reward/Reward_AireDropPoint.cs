using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Rewards
{
    [CreateAssetMenu(fileName = "AirDropPoint", menuName = "Simemes/Reward/Air Drop Point")]
    public class Reward_AirDropPoint : RewardConfig
    {
        public override void Obtain(int count = 1)
        {
            base.Obtain(count);
        }
    }
}
