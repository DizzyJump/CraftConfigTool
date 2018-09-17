using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
    ItemInfo Item;
    public RectTransform DragTransform;
    public static ItemInfo itemBeingDragged;
    Transform startParent;
    Vector3 startPosition;
    public CraftEngine engine;
    public ErrorMessage Message;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = Item;
        startPosition = DragTransform.position;
        transform.SetAsLastSibling();
        Item.isNeedUpdate = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(itemBeingDragged != null)
            DragTransform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        string WorkItemID = Item.ID;
        string ResourceID = itemBeingDragged.ID;
        if(engine.isHaveResourceCycle(WorkItemID, ResourceID))
            Message.Show("ERROR", "Resource graph cycle detected!");
        else
            AddResourcePanel.Open(WorkItemID, ResourceID);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(itemBeingDragged != null)
        {
            itemBeingDragged = null;
            DragTransform.position = startPosition;
        }
        Item.isNeedUpdate = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    // Use this for initialization
    void Start () {
        Item = GetComponent<ItemInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
