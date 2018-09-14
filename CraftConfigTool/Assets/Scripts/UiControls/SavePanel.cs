using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Crosstales.FB;
using System.IO;

public class SavePanel : MonoBehaviour {
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

    public void OnClick()
    {
        string json = engine.Serialize(input.text);
        string path = FileBrowser.SaveFile("Save file", "", "default config name", "json");
        File.WriteAllText(path, json);
        gameObject.SetActive(false);
    }
}
