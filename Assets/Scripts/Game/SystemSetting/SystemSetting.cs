using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

namespace Simemes
{
    public class SystemSetting : MonoSingleton<SystemSetting>
    {
        [SerializeField] SettingConfig _settingConfig;

        public static SettingConfig Config => instance._settingConfig;
    }
}
