using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WelcomePanel : MonoBehaviour {
    Dictionary<string, int> builded = new Dictionary<string, int>();
    Dictionary<int, Dictionary<string, int>> grinded = new Dictionary<int, Dictionary<string, int>>();
    public bool NeedSave = true;

    public RectTransform BuildedAnchor;
    public RectTransform GrindedAnchor;
    public Transform ItemPrefab;
    public Transform RowPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnable()
    {
        UpdateView();
    }

    void UpdateView()
    {
        // update builded
        while(BuildedAnchor.childCount>0)
        {
            var item = BuildedAnchor.GetChild(0);
            item.SetParent(null);
            Destroy(item.gameObject);
        }
        foreach(var res in builded)
        {
            var item = Instantiate(ItemPrefab, BuildedAnchor);
            item.localScale = Vector3.one;
            var item_controller = item.GetComponent<InventoryResourceIcon>();
            item_controller.Setup(res.Key, res.Value.ToString(), Color.white, null, false, false);
        }
        // update grinded
        for(int i = 0; i < GrindedAnchor.childCount; i++)
        {
            Destroy(GrindedAnchor.GetChild(i).gameObject);
        }
        foreach(var res in grinded)
        {
            var row = Instantiate(RowPrefab, GrindedAnchor);
            row.localScale = Vector3.one;
            row.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Battle " + res.Key.ToString() + " info";
            foreach(var item_info in res.Value)
            {
                var item = Instantiate(ItemPrefab, row);
                item.localScale = Vector3.one;
                var item_controller = item.GetComponent<InventoryResourceIcon>();
                item_controller.Setup(item_info.Key, item_info.Value.ToString(), Color.white, null, false, false);
            }
        }
    }

    public void OnDisable()
    {
        builded.Clear();
        grinded.Clear();
    }

    public void AddBuild(string id, int count)
    {
        if(NeedSave)
        {
            if(!builded.ContainsKey(id))
                builded.Add(id, count);
            else
                builded[id] += count;
        }
    }

    public void AddBattle(int battle_num)
    {
        if(NeedSave)
        {
            grinded.Add(battle_num, new Dictionary<string, int>());
        }
    }

    public void AddGrindedRes(int battle_num, string id, int count)
    {
        if(grinded.ContainsKey(battle_num))
        {
            if(!grinded[battle_num].ContainsKey(id))
                grinded[battle_num].Add(id, count);
            else
                grinded[battle_num][id] += count;
        }
    }

    public bool isNeedOpen()
    {
        return builded.Count > 0 || grinded.Count > 0;
    }
}
