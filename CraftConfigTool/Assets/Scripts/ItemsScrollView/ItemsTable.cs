using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsTable : MonoBehaviour {
    public CraftEngine engine;
    int CurrentActiveColumns = -1;
    List<Column> Columns = new List<Column>();
    public Transform ColumnPrefab;
    public ItemsCache ItemsCache;
	// Use this for initialization
	void Start () {
        engine.Clear();
        ItemsCache.Reset();
    }
	
	// Update is called once per frame
	void Update () {
		if(CurrentActiveColumns != engine.GetCraftGraphDeepness())
        {
            RebuildColumns();
        }
	}

    public void RebuildColumns()
    {
        engine.UpdateCaches();
        int CurrentDeepness = engine.GetCraftGraphDeepness();
        if(CurrentActiveColumns > CurrentDeepness) // надо убрать колонки
        {
            for(int i=CurrentDeepness; i<CurrentActiveColumns; i++)
                Columns[i].Hide();
        }
        else // надо добавить колонки
        {
            while(CurrentDeepness > Columns.Count) // если надо создаём новые объекты колонок
            {
                var obj = Instantiate(ColumnPrefab);
                obj.transform.SetParent(transform);
                obj.transform.localScale = Vector3.one;
                obj.transform.SetSiblingIndex(Columns.Count);
                Column col = obj.GetComponent<Column>();
                col.Level = Columns.Count;
                Columns.Add(col);
                col.Show();
            }
        }
        CurrentActiveColumns = CurrentDeepness;
        for(int i = 0; i < CurrentDeepness; i++)
            Columns[i].Rebuild();
    }
}
