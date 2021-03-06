﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

//[CreateAssetMenu]
public class CraftEngine : ScriptableObject {
    [System.Serializable]
    class craft_system_config
    {
        public string config_name;
        [JsonProperty("serialized_at")]
        public DateTime date;
        [JsonProperty("items_list")]
        public List<CraftItem> Items = new List<CraftItem>();
    }

    craft_system_config config = new craft_system_config();
    private Dictionary<string, CraftItem> id_map = new Dictionary<string, CraftItem>(); // пока так, если будет тормозить заменю на цифровой индекс
    private Dictionary<string, int> DeepnessCache = new Dictionary<string, int>();
    private Dictionary<int, List<CraftItem>> LevelsCache = new Dictionary<int, List<CraftItem>>();
    private List<string> ResourcesCache = new List<string>();
    int GraphDeepness = 0;

    public EventObject UpdateViewEvent;

    public void Clear()
    {
        config = new craft_system_config();
        id_map.Clear();
    }

    public CraftItem AddItem(string id)
    {
        if(ContainsItem(id))
            return null;
        CraftItem new_item = new CraftItem();
        new_item.ItemTypeId = id;
        config.Items.Add(new_item);
        id_map.Add(id, new_item);
        UpdateCaches();
        return new_item;
    }

    public bool ContainsItem(string id)
    {
        return id_map.ContainsKey(id);
    }

    public CraftItem GetItem(string id)
    {
        CraftItem item = null;
        id_map.TryGetValue(id, out item);
        return item;
    }

    public void SetCraftTime(string id, float time)
    {
        var item = GetItem(id);
        if(item != null)
            item.CraftTime = time;
    }

    public void RemoveItem(string id)
    {
        if(ContainsItem(id))
        {
            var item = GetItem(id);
            config.Items.Remove(item);
            id_map.Remove(id);
            for(int item_index=0;item_index< config.Items.Count; item_index++)
            {
                var WorkItem = config.Items[item_index];
                WorkItem.CraftCosts.Remove(id);
            }
            UpdateCaches();
            UpdateViewEvent.Invoke();
        }
    }

    public void SetCraftCost(string item_id, string cost_id, int count)
    {
        if(ContainsItem(item_id) && ContainsItem(cost_id) && item_id != cost_id && !isHaveResourceCycle(item_id, cost_id))
        {
            var WorkItem = GetItem(item_id);
            WorkItem.CraftCosts[cost_id] = count;
            UpdateCaches();
            UpdateViewEvent.Invoke();
        }
    }

    public bool isHaveResourceCycle(string search_item_id, string check_item_id)
    {
        var Item = GetItem(check_item_id);
        foreach(var res in Item.CraftCosts)
        {
            if(res.Key == search_item_id)
                return true;
            else
                return isHaveResourceCycle(search_item_id, res.Key);
        }
        return false;
    }

    public void RemoveCraftCost(string item_id, string cost_id)
    {
        if(ContainsItem(item_id))
        {
            var WorkItem = GetItem(item_id);
            WorkItem.CraftCosts.Remove(cost_id);
            UpdateCaches();
            UpdateViewEvent.Invoke();
        }
    }

    public string Serialize(string name="Default name")
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Auto;
        config.config_name = name;
        config.date = DateTime.UtcNow;
        string json = JsonConvert.SerializeObject(config, Formatting.Indented, settings);
        return json;
    }

    public List<string> GetItemsList()
    {
        List<string> list = new List<string>();
        for(int i = 0; i < config.Items.Count; i++)
            list.Add(config.Items[i].ItemTypeId);
        return list;
    }

    public void UpdateCaches()
    {
        UpdateIdMapCache();
        UpdateDeepnessCache();
        UpdateResourcesCache();
    }

    void UpdateIdMapCache()
    {
        id_map.Clear();
        for(int i=0; i<config.Items.Count; i++)
        {
            CraftItem craftItem = config.Items[i];
            id_map.Add(craftItem.ItemTypeId, craftItem);
        }
    }

    void UpdateResourcesCache()
    {
        ResourcesCache.Clear();
        for(int i = 0; i < config.Items.Count; i++)
            if(config.Items[i].CraftCosts.Count == 0)
                ResourcesCache.Add(config.Items[i].ItemTypeId);
    }

    public List<string> GetResourcesList()
    {
        return ResourcesCache;
    }

    void UpdateDeepnessCache()
    {
        GraphDeepness = -1;
        DeepnessCache.Clear();
        LevelsCache.Clear();
        for(int i=0; i<config.Items.Count;i++)
        {
            CraftItem craftItem = config.Items[i];
            int value = _GetItemDeepness(craftItem.ItemTypeId);
            if(value > GraphDeepness)
                GraphDeepness = value;
            DeepnessCache.Add(craftItem.ItemTypeId, value);
            // апдейт LevelsCache
            if(!LevelsCache.ContainsKey(value))
                LevelsCache.Add(value, new List<CraftItem>());
            LevelsCache[value].Add(craftItem);
        }
    }

    public List<CraftItem> GetLevelItems(int level)
    {
        return LevelsCache.ContainsKey(level) ? LevelsCache[level] : null;
    }

    public int GetItemDeepness(string id)
    {
        int d = 0;
        DeepnessCache.TryGetValue(id, out d);
        return d;
    }

    public int GetCraftGraphDeepness()
    {
        return GraphDeepness+1;
    }

    private int _GetItemDeepness(string id)
    {
        var item = GetItem(id);
        int item_deepness = 0;
        foreach(var pair in item.CraftCosts)
        {
            int res_deepness = _GetItemDeepness(pair.Key);
            if(item_deepness <= res_deepness)
                item_deepness = res_deepness + 1;
        }
        return item_deepness;
    }

    public bool Deserialize(string json)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Auto;
        bool result = false;
        try
        {
            config = JsonConvert.DeserializeObject<craft_system_config>(json, settings);
            UpdateCaches();
            result = true;
            UpdateViewEvent.Invoke();
        }
        catch(Exception ex)
        {
            config.Items = new List<CraftItem>();
        }
        return result;
    }

    public void ChangeItemID(string old_id, string new_id)
    {
        var item = GetItem(old_id);
        item.ItemTypeId = new_id;
        id_map.Remove(old_id);
        id_map.Add(new_id, item);
        for(int item_index = 0; item_index<config.Items.Count; item_index++)
        {
            var WorkItem = config.Items[item_index];
            if(WorkItem.CraftCosts.ContainsKey(old_id))
            {
                int value = WorkItem.CraftCosts[old_id];
                WorkItem.CraftCosts.Remove(old_id);
                WorkItem.CraftCosts.Add(new_id, value);
            }
        }
        UpdateCaches();
        UpdateViewEvent.Invoke();
    }
}
