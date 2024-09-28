using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simemes;

namespace Simemes.Tasks
{
    [CreateAssetMenu(fileName = "Level Task", menuName = "Simemes/Task/Level Task")]
    public class LevelTaskConfig : TaskConfig
    {
        public override int UpdateProgress(int current, int value)
        {
            return GameManager.instance.PlayerProfile.Level;
        }
    }
}