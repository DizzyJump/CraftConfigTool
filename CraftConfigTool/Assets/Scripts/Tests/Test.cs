using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour {
    public CraftEngine engine;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Button]
    void TestSerialize()
    {
        SetTestGraph();
        string json = engine.Serialize();
        Debug.Log(json);
        engine.Clear();
    }

    [Button]
    void SetTestGraph()
    {
        engine.Clear();
        string test_id = "test_id";
        string test_resource_id_1 = "test_resource_1";
        string test_resource_id_2 = "test_resource_2";
        string test_resource_id_3 = "test_resource_3";
        var item = engine.AddItem(test_id);
        engine.AddItem("test_resource_1");
        engine.AddItem("test_resource_2");
        engine.AddItem("test_resource_3");
        item.CraftTime = 5f;

        engine.SetCraftCost(test_id, test_resource_id_1, 10);
        engine.SetCraftCost(test_id, test_resource_id_2, 20);
        engine.SetCraftCost(test_id, test_resource_id_3, 30);

        engine.SetCraftCost(test_resource_id_1, test_resource_id_2, 5);

        item.Descriptors.ChooseToCraft = new ChooseToCraftItemDescriptors();
    }
}
