using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOrderChanger : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string ID;
    public CraftEngine engine;
    public Transform Anchor;

    public void OnDrop(PointerEventData eventData)
    {
        var itsItem = engine.GetItem(ID);
        var dragItem = ItemDragHandler.itemBeingDragged;
        if(engine.GetItemDeepness(ID) == engine.GetItemDeepness(dragItem.ID) && ID != dragItem.ID)
        {
            dragItem.Anchor.SetSiblingIndex(Anchor.GetSiblingIndex());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*var itsItem = engine.GetItem(ID);
        var dragItem = ItemDragHandler.itemBeingDragged;
        pointer_enter_subling_index = dragItem.Anchor.GetSiblingIndex();
        if(engine.GetItemDeepness(ID) == engine.GetItemDeepness(dragItem.ID) && ID != dragItem.ID)
        {
            dragItem.Anchor.SetSiblingIndex(Anchor.GetSiblingIndex());
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /*var itsItem = engine.GetItem(ID);
        var dragItem = ItemDragHandler.itemBeingDragged;
        if(engine.GetItemDeepness(ID) == engine.GetItemDeepness(dragItem.ID) && ID != dragItem.ID)
        {
            dragItem.Anchor.SetSiblingIndex(pointer_enter_subling_index);
        }*/
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
