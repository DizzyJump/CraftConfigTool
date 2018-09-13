﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class ItemsCache : ScriptableObject {
    Dictionary<string, ItemInfo> Cache = new Dictionary<string, ItemInfo>();
    public ItemInfo ItemPrefab;

    public void Reset()
    {
        foreach(var item in Cache)
            Destroy(item.Value);
        Cache.Clear();
    }

    public void Create(string id)
    {
        var parent = GameObject.FindGameObjectWithTag("ItemsParent");
        var item = Instantiate(ItemPrefab);
        item.transform.SetParent(parent.transform);
        item.transform.position = Vector3.zero;
        item.SetItem(id);
        Cache.Add(id, item);
    }

    public void Remove(string id)
    {
        Destroy(Cache[id].gameObject);
        Cache.Remove(id);
    }

    public ItemInfo Get(string id)
    {
        if(!Cache.ContainsKey(id))
            Create(id);
        return Cache[id];
    }
}
