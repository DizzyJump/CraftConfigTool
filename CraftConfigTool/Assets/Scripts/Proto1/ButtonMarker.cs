using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMarker : MonoBehaviour {
    Image Icon;
    public InventoryData Inventory;
    public CraftEngine engine;
    List<CraftItem> Items = new List<CraftItem>();
	// Use this for initialization
	void Start () {
        Icon = GetComponent<Image>();
        var raw_list = engine.GetItemsList();
        for(int i=0;i< raw_list.Count;i++)
        {
            var item = engine.GetItem(raw_list[i]);
            if(item.CraftCosts.Count > 0)
                Items.Add(item);
        }
	}
	
	// Update is called once per frame
	void Update () {
        int can_craft = 0;
        for(int i=0; i<Items.Count; i++)
        {
            if(Inventory.CanBeBuild(Items[i]))
                can_craft++;
        }
        Icon.enabled = can_craft > 0;
	}
}
