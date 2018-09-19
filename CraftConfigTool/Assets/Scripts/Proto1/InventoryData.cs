using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryData : ScriptableObject {
    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    public void Add(string res, int count)
    {
        if(!inventory.ContainsKey(res))
            inventory.Add(res, count);
        else
            inventory[res] += count;
    }

    public void Spend(string res, int count)
    {
        inventory[res] -= count;
    }

    public bool CanBeBuild(CraftItem item)
    {
        bool result = item.CraftCosts.Count > 0;
        foreach(var res in item.CraftCosts)
            result = result && inventory[res.Key] >= res.Value;
        return result;
    }
}
