using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryResourceIcon : MonoBehaviour {
    public Image Icon;
    public Button IconButton;
    public TextMeshProUGUI Value;
    public CraftEngine engine;
    public IconSet icon_set;
    public Image BuildableMarker;
    public Image ResourcesEnoughtMarker;
    public Image CanBeBuildMarker;
    public InventoryData Inventory;

    public void Setup(string res_id, string value, Color value_color, UnityAction action, bool res_enougth)
    {
        var item = engine.GetItem(res_id);
        var sprite = icon_set.Get(item.IconName);
        BuildableMarker.enabled = item.CraftCosts.Count > 0;
        ResourcesEnoughtMarker.enabled = res_enougth;
        CanBeBuildMarker.enabled = !res_enougth && CanBeBuild(item);
        SetupEx(res_id, sprite, value, value_color, item.CraftCosts.Count>0 ? action : null);
    }

    bool CanBeBuild(CraftItem item)
    {
        return Inventory.CanBeBuild(item);
    }

    void SetupEx(string res_id, Sprite sprite, string value, Color value_color, UnityAction action)
    {
        IconButton.interactable = action != null;
        IconButton.onClick.RemoveAllListeners();
        if(action!=null)
            IconButton.onClick.AddListener(action);
        Icon.sprite = sprite;
        Value.text = value;
        Value.color = value_color;
    }
}
