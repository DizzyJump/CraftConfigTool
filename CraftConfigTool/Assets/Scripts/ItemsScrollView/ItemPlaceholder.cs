using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPlaceholder : MonoBehaviour {
    string ID;
    ItemInfo WorkItem;
    public ItemsCache ItemsCache;
    LayoutElement layout;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(WorkItem!=null && layout!=null)
        {
            RectTransform tr = WorkItem.transform as RectTransform;
            layout.preferredWidth = tr.rect.width;
            layout.preferredHeight = tr.rect.height;
        }
    }

    public void SetItem(string id, Transform column_transform)
    {
        if(layout == null)
            layout = GetComponent<LayoutElement>();
        ID = id;
        transform.SetParent(column_transform);
        WorkItem = ItemsCache.Get(ID);
        WorkItem.SetAnchor(transform);
    }

    public void Hide()
    {
        WorkItem = null;
        ID = "";
        gameObject.SetActive(false);
    }
}
