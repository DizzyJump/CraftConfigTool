﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Crosstales.FB;
using UnityEngine;
using UnityEngine.UI;

public class GenerateResources : MonoBehaviour {
    public Transform ResourceIconPrefab;
    public Transform Target;
    public int MinCount;
    public int MaxCount;
    public float GenerateDelay;
    public IconSet IconsSet;
    public InventoryData Inventory;
    public CraftEngine engine;
    int BattleCount = 1;
    public WelcomePanel welcome;
	// Use this for initialization
	void Start () {
        var item_list = engine.GetItemsList();
        for(int i=0; i<item_list.Count; i++)
        {
            Inventory.Add(item_list[i], 0);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Generate()
    {
        StartCoroutine(GenerateWorker(BattleCount++, Random.Range(MinCount, MaxCount)));
    }

    IEnumerator GenerateWorker(int battle, int count)
    {
        welcome.AddBattle(battle);
        while(true)
        {
            yield return new WaitForSeconds(GenerateDelay * Random.Range(0.8f, 1.0f));
            if(count>0)
            {
                var obj = Instantiate(ResourceIconPrefab);
                obj.SetParent(transform.parent);
                obj.position = transform.position;
                obj.localScale = Vector3.one;
                ResourceIconContorller ctr = obj.gameObject.GetComponent<ResourceIconContorller>();
                var resources = engine.GetResourcesList();
                int resource_index = Random.Range(0, resources.Count);
                var item = engine.GetItem(resources[resource_index]);
                obj.GetComponent<Image>().sprite = IconsSet.Get(item.IconName);
                welcome.AddGrindedRes(battle, resources[resource_index], 1);
                Inventory.Add(resources[resource_index], 1);
                ctr.Target = Target as RectTransform;
                count--;
            }
        }
    }
}
