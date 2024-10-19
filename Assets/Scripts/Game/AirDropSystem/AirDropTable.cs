using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AirDropTable
{
    private Dictionary<int, Dictionary<int,int>> _table;

    public void Init(string json)
    {
        _table = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, int>>>(json);
    }

    public int GetRandomItem(int tier)
    {
        _table.TryGetValue(tier, out var dropItems);

        int totalWeight = 0;

        foreach (var drop in dropItems)
            totalWeight += drop.Value;

        int randomValue = Random.Range(0, totalWeight);

        foreach (var drop in dropItems)
        {
            if (randomValue < drop.Value)
                return drop.Key;

            randomValue -= drop.Value;
        }

        return -1;
    }
}
