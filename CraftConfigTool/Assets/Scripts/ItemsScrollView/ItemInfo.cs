using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfo : MonoBehaviour {
    public CraftEngine engine;
    public TextMeshProUGUI ItemIdText;
    Transform Anchor;
    public float Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, Anchor.position, Speed * Time.deltaTime);
	}

    public void SetItem(string id)
    {
        ItemIdText.text = id;
    }

    public void SetAnchor(Transform anchor)
    {
        Anchor = anchor;
    }
}
