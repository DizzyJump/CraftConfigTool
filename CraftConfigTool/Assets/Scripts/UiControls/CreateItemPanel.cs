using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateItemPanel : MonoBehaviour {
    public CraftEngine engine;
    public TMP_InputField input;
    public Button button;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        button.interactable = input.text != "";
    }

    public void OnCreate()
    {
        var item = engine.AddItem(input.text);
        gameObject.SetActive(false);
    }
}
