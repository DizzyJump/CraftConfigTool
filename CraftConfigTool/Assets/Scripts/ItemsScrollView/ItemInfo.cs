using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour {
    public CraftEngine engine;
    public TMP_InputField ItemIdText;
    public TMP_InputField CraftTimeText;
    public Image ItemIcon;
    public Transform Anchor;
    public float Speed;
    public string ID;
    public ErrorMessage MessagEvent;
    public ItemsCache ItemCache;
    public RectTransform ResourceGrid;
    public Transform ResourceCellPrefab;
    public IconSet Icons;
    public EventObject OnUpdateItemsEvent;
    public ChooseIconInfo IconChangeEvent;
    public bool isNeedUpdate = true;
    public ItemOrderChanger OrderChanger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Anchor!=null && isNeedUpdate)
            transform.position = Vector3.MoveTowards(transform.position, Anchor.position, Speed * Time.deltaTime);
	}

    public void SetItem(string id)
    {
        ID = id;
        OrderChanger.ID = ID;
        UpdateView();
    }

    public void UpdateView()
    {
        var item = engine.GetItem(ID);
        ItemIdText.text = ID;
        CraftTimeText.text = item.CraftTime.ToString();
        UpdateCraftTimeTextColor();
        ItemIcon.sprite = Icons.Get(item.IconName);
        UpdateResources(item);
    }

    void UpdateCraftTimeTextColor()
    {
        var item = engine.GetItem(ID);
        var colors = CraftTimeText.colors;
        colors.disabledColor = item.CraftCosts.Count > 0 && item.CraftTime == 0 ? Color.red : Color.white;
        CraftTimeText.colors = colors;
    }

    void UpdateResources(CraftItem item)
    {
        int index = 0;
        // апдейтим/добавляем/включаем нужные
        foreach(var res in item.CraftCosts)
        {
            if(ResourceGrid.childCount< item.CraftCosts.Count) // если недостаточно ячеек - делаем новую
            {
                var new_cell = Instantiate(ResourceCellPrefab);
                new_cell.SetParent(ResourceGrid);
                new_cell.localScale = Vector3.one;
            }
            Transform cell_tr = ResourceGrid.GetChild(index);
            cell_tr.GetComponent<ResourceCellDescriptor>().Setup(ID, res.Key, res.Value, ()=>{ OnResourceDelete(res.Key); }); // прокидываем туда значения
            if(!cell_tr.gameObject.activeSelf) // если надо включаем
                cell_tr.gameObject.SetActive(true);
            index++;
        }
        // скрываем лишние
        for(int i=item.CraftCosts.Count; i < ResourceGrid.childCount; i++)
        {
            ResourceGrid.GetChild(i).gameObject.SetActive(false); 
        }
    }

    void OnResourceDelete(string resource_id)
    {
        engine.RemoveCraftCost(ID, resource_id);
    }

    public void SetAnchor(Transform anchor)
    {
        Anchor = anchor;
        OrderChanger.Anchor = Anchor;
    }

    public void OnChangeCraftTime()
    {
        string new_time_str = CraftTimeText.text;
        engine.SetCraftTime(ID, Mathf.Abs(System.Convert.ToInt32(new_time_str)));
        UpdateCraftTimeTextColor();
    }

    public void OnChangeID()
    {
        string new_id = ItemIdText.text;
        if(engine.ContainsItem(new_id))
        {
            MessagEvent.Show("Change ID error", "There is already exist item with specified ID: "+ new_id);
        }
        else
        {
            engine.ChangeItemID(ID, new_id);
            ItemCache.ChangeItemID(ID, new_id);
            ID = new_id;
        }
        UpdateView();
    }

    public void OnDelete()
    {
        engine.RemoveItem(ID);
        ItemCache.Remove(ID);
    }

    public void OnClickIcon()
    {
        var item = engine.GetItem(ID);
        IconChangeEvent.Invoke(item.IconName, OnSwitchIcon);
    }

    public void OnSwitchIcon(string sprite_name)
    {
        var item = engine.GetItem(ID);
        if(item.IconName!=sprite_name)
        {
            item.IconName = sprite_name;
            OnUpdateItemsEvent.Invoke();
        }
    }
}
