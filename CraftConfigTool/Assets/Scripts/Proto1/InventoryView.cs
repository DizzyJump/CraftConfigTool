using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour {
    public Transform ResourceItemPrefab;
    public InventoryData Inventory;
    List<InventoryResourceIcon> cells = new List<InventoryResourceIcon>();
    public BuildBox build_window;

	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        UpdateView();
    }

    public void UpdateView()
    {
        while(cells.Count < Inventory.inventory.Count)
        {
            var cell = Instantiate(ResourceItemPrefab, transform);
            cell.localScale = Vector3.one;
            cells.Add(cell.GetComponent<InventoryResourceIcon>());
        }
        int i = 0;
        foreach(var item in Inventory.inventory)
        {
            cells[i].Setup(item.Key, item.Value.ToString(), Color.white, () => { build_window.Setup(item.Key); }, false);
            i++;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
