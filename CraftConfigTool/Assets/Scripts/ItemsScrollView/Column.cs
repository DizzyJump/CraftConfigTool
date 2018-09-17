using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {
    public int Level = 0;
    public Transform ItemCellPrefab;
    public CraftEngine engine;
    List<ItemPlaceholder> FreeItems = new List<ItemPlaceholder>();
    List<ItemPlaceholder> ColumnItems = new List<ItemPlaceholder>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show()
    {
        Rebuild();
        gameObject.SetActive(true);
    }

    public void Rebuild()
    {
        // устанавливаем праивльное количество
        List<CraftItem> engine_list = engine.GetLevelItems(Level);
        if(engine_list!=null && ColumnItems.Count != engine_list.Count)
        {
            if(ColumnItems.Count < engine_list.Count) // надо добавлять
            {
                while(ColumnItems.Count < engine_list.Count)
                {
                    if(FreeItems.Count>0)
                    {
                        ColumnItems.Add(FreeItems[0]);
                        FreeItems.RemoveAt(0);
                    }
                    else
                    {
                        var new_item = Instantiate(ItemCellPrefab);
                        new_item.localScale = Vector3.one;
                        ColumnItems.Add(new_item.GetComponent<ItemPlaceholder>());
                    }
                }
            }
            else // надо убавлять
            {
                while(ColumnItems.Count > engine_list.Count)
                {
                    int tail_index = ColumnItems.Count - 1;
                    ItemPlaceholder item = ColumnItems[tail_index];
                    item.Hide();
                    FreeItems.Add(item);
                    ColumnItems.RemoveAt(tail_index);
                }
            }
        }

        if(engine_list!=null)
        {
            for(int i=0; i<ColumnItems.Count; i++)
            {
                ColumnItems[i].SetItem(engine_list[i].ItemTypeId, transform);
            }
        }
    }

    public void Hide()
    {
        while(ColumnItems.Count>0)
        {
            int tail_index = ColumnItems.Count - 1;
            ColumnItems[tail_index].Hide();
            FreeItems.Add(ColumnItems[tail_index]);
            ColumnItems.RemoveAt(tail_index);
        }
        gameObject.SetActive(false);
    }
}
