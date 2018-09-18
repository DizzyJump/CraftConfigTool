using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResourceCellDescriptor : MonoBehaviour {
    public Image Icon;
    public Button IconButton;
    public TextMeshProUGUI Value;
    public CraftEngine engine;
    public Button DeleteButton;
    public IconSet icon_set;
	
    public void Setup(string item_id, string res_id, int value, UnityAction action_on_delete)
    {
        var item = engine.GetItem(res_id);
        var sprite = icon_set.Get(item.IconName);
        SetupEx(item_id, res_id, sprite, value.ToString(), action_on_delete);
    }

    void SetupEx(string item_id, string res_id, Sprite sprite, string value, UnityAction action_on_delete)
    {
        if(action_on_delete==null)
        {
            DeleteButton.onClick.RemoveAllListeners();
            DeleteButton.onClick.AddListener(action_on_delete);
            IconButton.onClick.RemoveAllListeners();
            IconButton.onClick.AddListener(() => { AddResourcePanel.Open(item_id, res_id); });
        }
        Icon.sprite = sprite;
        Value.text = value;
    }
}
