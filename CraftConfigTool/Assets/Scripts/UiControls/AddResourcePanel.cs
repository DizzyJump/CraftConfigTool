using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AddResourcePanel : MonoBehaviour {
    public TextMeshProUGUI ItemID;
    public TextMeshProUGUI ResourceID;
    public TMP_InputField Value;
    static AddResourcePanel panel;
    public CraftEngine engine;
    public Button button;

    string PrevValue = "0";
    // Use this for initialization
    void Start () {
        panel = this;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            button.interactable = Value.text != "" && System.Convert.ToInt32(Value.text) > 0;
        }
        catch(Exception ex)
        {

        }
    }

    public static void Open(string item_id, string resource_id)
    {
        panel.OpenEx(item_id, resource_id);
    }

    void OpenEx(string item_id, string resource_id)
    {
        PrevValue = "0";
        var item = engine.GetItem(item_id);
        ItemID.text = item_id;
        ResourceID.text = resource_id;
        if(item.CraftCosts.ContainsKey(resource_id))
            PrevValue = item.CraftCosts[resource_id].ToString();
        else
            PrevValue = "0";
        Value.text = PrevValue;
        gameObject.SetActive(true);
    }

    public void OnClickSet()
    {
        engine.SetCraftCost(ItemID.text, ResourceID.text, System.Convert.ToInt32(Value.text));
        gameObject.SetActive(false);
    }
}
