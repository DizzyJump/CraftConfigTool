using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildPipelineCell : MonoBehaviour {
    public enum States
    {
        Queue,
        FirstInQueue,
        Slot
    }

    public Image ItemIcon;
    public Slider Progress;
    public Button UnlockNewSlot;
    public CraftEngine engine;
    public IconSet Icons;
    public string ID;
    public TextMeshProUGUI timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateState(States state)
    {
        switch(state)
        {
            case States.Slot:
                Progress.gameObject.SetActive(true);
                UnlockNewSlot.gameObject.SetActive(false);
                break;
            case States.Queue:
                Progress.gameObject.SetActive(false);
                UnlockNewSlot.gameObject.SetActive(false);
                break;
            case States.FirstInQueue:
                Progress.gameObject.SetActive(false);
                UnlockNewSlot.gameObject.SetActive(true);
                break;
        }
    }

    public float UpdateProgress(float increment, float CurrentSpeed)
    {
        Progress.value += increment;
        float delta = Mathf.Max(Progress.maxValue - Progress.value, 0);
        delta = delta / CurrentSpeed;
        timer.text = delta.ToString("F0") + "s";
        return Progress.normalizedValue;
    }

    public void Setup(string item_id, UnityAction action)
    {
        ID = item_id;
        var item = engine.GetItem(item_id);
        ItemIcon.sprite = Icons.Get(item.IconName);
        Progress.maxValue = item.CraftTime;
        UnlockNewSlot.onClick.RemoveAllListeners();
        UnlockNewSlot.onClick.AddListener(action);
        UpdateState(States.Queue);
    }
}
