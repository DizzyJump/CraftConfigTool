using System.Collections;
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
        }
    }

    public void SetCraftCost(string item_id, string cost_id, int count)
    {
        if(ContainsItem(item_id))
        {
            var WorkItem = GetItem(item_id);
            var CostItem = GetItem(cost_id);
            WorkItem.CraftCosts[cost_id] = count;
            WorkItem.Deepness = WorkItem.Deepness > CostItem.Deepness ? WorkItem.Deepness : CostItem.Deepness + 1;
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

    public bool Deserialize(string json)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Auto;
        bool result = false;
        try
        {
            config = JsonConvert.DeserializeObject<craft_system_config>(json, settings);
            id_map = new Dictionary<string, CraftItem>();
            for(int i=0; i<config.Items.Count; i++) // rebuild id->obj cache
            {
                var WorkItem = config.Items[i];
                id_map.Add(WorkItem.ItemTypeId, WorkItem);
            }
            result = true;
        }
        catch(Exception ex)
        {
            config.Items = new List<CraftItem>();
        }
        return result;
    }
}
