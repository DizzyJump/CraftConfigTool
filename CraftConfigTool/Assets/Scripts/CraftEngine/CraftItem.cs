using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public interface IItemDescriptors
{

}

[System.Serializable]
public class UniqueItemDescriptors : IItemDescriptors
{

}

[System.Serializable]
public class PrizeBoneDiscountItemDescriptors : IItemDescriptors
{

}

[System.Serializable]
public class PrizeTankOpenItemDescriptors : IItemDescriptors
{

}

[System.Serializable]
public class TankUpgradeDescriptors : IItemDescriptors
{

}

[System.Serializable]
public class ChooseToCraftItemDescriptors : IItemDescriptors
{

}

[System.Serializable]
public class ItemDescriptors
{
    [JsonProperty("unique")]
    public UniqueItemDescriptors Unique = null;
    [JsonProperty("prize_bone_discount")]
    public PrizeBoneDiscountItemDescriptors PrizeBoneDiscount = null;
    [JsonProperty("prize_tank_open")]
    public PrizeTankOpenItemDescriptors PrizeTankOpen = null;
    [JsonProperty("tank_upgrade")]
    public TankUpgradeDescriptors TankUpgrade = null;
    [JsonProperty("choose_to_craft")]
    public ChooseToCraftItemDescriptors ChooseToCraft = null;
}

[System.Serializable]
public class CraftItem {
    [JsonProperty("item_id")]
    public string ItemTypeId;
    [JsonProperty("craft_time")]
    public float CraftTime;
    [JsonProperty("descriptors")]
    public ItemDescriptors Descriptors = new ItemDescriptors();
    [JsonProperty("craft_costs")]
    public Dictionary<string, int> CraftCosts = new Dictionary<string, int>();

    [JsonIgnore]
    public int Deepness = 0;
}
