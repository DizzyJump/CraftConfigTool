using System.Collections;
using System.Collections.Generic;
using Crosstales.FB;
using UnityEngine;
using System.IO;

public class LoadButton : MonoBehaviour {
    public CraftEngine engine;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        string extensions = "";
        string json = "";
        string path = FileBrowser.OpenSingleFile("Open File", "", extensions);
        if(path!="")
        {
            json = File.ReadAllText(path);
            Debug.Log(json);
            //if(json != "")
            //    engine.Deserialize(json);
        }
        
    }
}
