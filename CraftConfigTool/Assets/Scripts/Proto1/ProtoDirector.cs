using System.Collections;
using System.Collections.Generic;
using System.IO;
using Crosstales.FB;
using UnityEngine;

public class ProtoDirector : MonoBehaviour {
    public BuildPipeline Pipeline;
    public CraftEngine engine;
	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        string extensions = "";
        string json = "";
        string path = FileBrowser.OpenSingleFile("Open File", "", extensions);
        if(path != "")
        {
            json = File.ReadAllText(path);
            //Debug.Log(json);
            if(json != "")
                engine.Deserialize(json);
        }
        else
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update () {
        Pipeline.UpdateProgress(Time.deltaTime);
    }
}
