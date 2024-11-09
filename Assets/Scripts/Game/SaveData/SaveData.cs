using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simemes.Profile;
using Simemes.Collection;
using Simemes.Inventory;

public class SaveData
{
    public PlayerProfile Profile = new PlayerProfile();
    public PlayerCollection Collection = new PlayerCollection();
    public Inventory Inventory = new Inventory();

    public void Init()
    {
        Profile.Init();
    }
}
