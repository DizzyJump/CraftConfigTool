using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCellDescriptor : MonoBehaviour {
    public Image Icon;
    public TextMeshProUGUI Value;
    public CraftEngine engine;
    public IconSet icon_set;
	
    public void Setup(string res_id, int value)
    {
        var item = engine.GetItem(res_id);
        var sprite = icon_set.Get(item.IconName);
        SetupEx(sprite, value.ToString());
    }

    void SetupEx(Sprite sprite, string value)
    {
        Icon.sprite = sprite;
        Value.text = value;
    }
}
