using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes
{
    [CreateAssetMenu(fileName = "SettingConfig", menuName = "Simemes/SystemSetting/SettingConfig")]
    public class SettingConfig : ScriptableObject
    {
        public int RewindCount = 1;
    }
}
