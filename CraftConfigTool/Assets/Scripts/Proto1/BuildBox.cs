using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildBox : MonoBehaviour {
    public Image BuildItemIcon;
    public RectTransform ResourceAnchor;
    public Transform ResourcePrefab;
    public CraftEngine engine;
    public IconSet Icons;
    Stack<string> BackStack = new Stack<string>();
    public InventoryData Inventory;
    public Button BuildButton;
    public BuildPipeline Pipeline;
    public InventoryView InvView;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(string id)
    {
        BackStack.Push(id);
        UpdateView(id);
        gameObject.SetActive(true);
    }

    void UpdateView(string id)
    {
        var item = engine.GetItem(id);
        BuildItemIcon.sprite = Icons.Get(item.IconName);
        for(int i=0; i< ResourceAnchor.childCount;i++)
        {
            var go = ResourceAnchor.GetChild(i).gameObject;
            Destroy(go);
        }
        bool build_res = true;
        foreach(var res in item.CraftCosts)
        {
            var res_obj = Instantiate(ResourcePrefab, ResourceAnchor);
            res_obj.localScale = Vector3.one;
            var cell = res_obj.gameObject.GetComponent<InventoryResourceIcon>();
            int res_have = Inventory.inventory[res.Key];
            int res_need = res.Value;
            bool enougth_res = res_need <= res_have;
            build_res = build_res && enougth_res;
            string value_str = res_have.ToString() + "/" + res_need.ToString();
            cell.Setup(res.Key, value_str, enougth_res ? Color.green:Color.red, ()=> { Setup(res.Key); }, enougth_res);
        }
        BuildButton.interactable = build_res && item.CraftCosts.Count > 0;
    }

    public void UpdateView()
    {
        if(BackStack.Count>0)
            UpdateView(BackStack.Peek());
    }

    private void OnEnable()
    {
        UpdateView(BackStack.Peek());
    }

    public void Back()
    {
        BackStack.Pop();
        if(BackStack.Count == 0)
        {
            InvView.UpdateView();
            gameObject.SetActive(false);
        }            
        else
            UpdateView(BackStack.Peek());
    }

    public void StartBuild()
    {
        Pipeline.AddItemToBuild(BackStack.Peek());
        var item = engine.GetItem(BackStack.Peek());
        foreach(var res in item.CraftCosts)
        {
            Inventory.Spend(res.Key, res.Value);
        }
        UpdateView(BackStack.Peek());
        InvView.UpdateView();
    }
}
