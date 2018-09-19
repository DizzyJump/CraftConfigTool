using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPipeline : MonoBehaviour {
    public RectTransform PipelineAnchor;
    public Transform ItemPrefab;
    List<BuildPipelineCell> pipeline = new List<BuildPipelineCell>();
    public float ProgressSpeed;
    public InventoryData Inventory;
    public float SpeedIncrementStep;
    int Slots = 1;
    List<BuildPipelineCell> finish_list = new List<BuildPipelineCell>();

    public InventoryView InvView;
    public BuildBox BuildArea;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        UpdateStates();
    }

    public void UpdateProgress(float dt)
    {
        finish_list.Clear();
        for(int i = 0; i < Slots && i<pipeline.Count; i++)
            if(pipeline[i].UpdateProgress(dt * ProgressSpeed, ProgressSpeed) >=1f)
            {
                finish_list.Add(pipeline[i]);
            }
        while(finish_list.Count>0)
        {
            BuildPipelineCell finished_cell = finish_list[0];
            Inventory.Add(finished_cell.ID, 1);
            pipeline.Remove(finished_cell);
            finish_list.RemoveAt(0);
            Destroy(finished_cell.gameObject);
            UpdateStates();
            InvView.UpdateView();
            BuildArea.UpdateView();
        }
    }

    public void AddItemToBuild(string id)
    {
        var new_item = Instantiate(ItemPrefab, PipelineAnchor);
        new_item.localScale = Vector3.one;
        BuildPipelineCell item_contoller = new_item.GetComponent<BuildPipelineCell>();
        item_contoller.Setup(id, ()=> { AddSlot(); });
        /*if(pipeline.Count == Slots)
            item_contoller.UpdateState(BuildPipelineCell.States.FirstInQueue);
        if(pipeline.Count<Slots)
            item_contoller.UpdateState(BuildPipelineCell.States.Slot);*/
        pipeline.Add(item_contoller);
        UpdateStates();
    }

    void UpdateStates()
    {
        for(int i = 0; i < Slots && i < pipeline.Count; i++)
            pipeline[i].UpdateState(BuildPipelineCell.States.Slot);
        if(pipeline.Count > Slots)
            pipeline[Slots].UpdateState(BuildPipelineCell.States.FirstInQueue);
    }

    public void AddSlot()
    {
        //pipeline[Slots].UpdateState(BuildPipelineCell.States.Slot);
        Slots++;
        /*if(pipeline.Count>Slots)
            pipeline[Slots].UpdateState(BuildPipelineCell.States.FirstInQueue);*/
        UpdateStates();
    }

    public void IncreaseSpeed()
    {
        ProgressSpeed += SpeedIncrementStep;
    }
}
