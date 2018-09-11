using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftInfo
{
    public string item_id;
    public int demand_count;
}

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
public class CraftDescriptors
{
    public UniqueItemDescriptors Unique = null;
    public PrizeBoneDiscountItemDescriptors PrizeBoneDiscount = null;
    public PrizeTankOpenItemDescriptors PrizeTankOpen = null;
    public TankUpgradeDescriptors TankUpgrade = null;
    public ChooseToCraftItemDescriptors ChooseToCraft = null;
}

[System.Serializable]
public class CraftItem {
    public string ItemTypeId;
    public float CraftTime;
    public CraftDescriptors Descriptors;
    public CraftInfo[] CraftInfo;
}
