using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomePanelOpener : MonoBehaviour {
    public WelcomePanel Panel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        Panel.NeedSave = false;
        if(Panel.isNeedOpen())
            Panel.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Panel.NeedSave = true;
    }
}
