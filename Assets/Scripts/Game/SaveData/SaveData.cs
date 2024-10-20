using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simemes.Profile;
using Simemes.Collection;

public class SaveData
{
    public PlayerProfile Profile = new PlayerProfile();
    public PlayerCollection Collection = new PlayerCollection();

    public void Init()
    {
        Profile.Init();
    }
}
