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
}
