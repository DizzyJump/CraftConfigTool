using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHotkeys : MonoBehaviour {
    ScrollRect scroll;
    public float Speed;
	// Use this for initialization
	void Start () {
        scroll = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.W))
            scroll.verticalNormalizedPosition += Speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.S))
            scroll.verticalNormalizedPosition -= Speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.D))
            scroll.horizontalNormalizedPosition += Speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.A))
            scroll.horizontalNormalizedPosition -= Speed * Time.deltaTime;
    }
}
