using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseIconPanel : MonoBehaviour {
    public IconSet Icons;
    public Transform ChooseButtonPrefab;
    public Transform GridTransform;
	// Use this for initialization
	void Start () {
		for(int i=0; i<Icons.sprites.Length; i++)
        {
            Sprite WorkSprite = Icons.sprites[i];
            var new_button = Instantiate(ChooseButtonPrefab);
            new_button.SetParent(GridTransform);
            new_button.localScale = Vector3.one;
            new_button.GetComponent<Image>().sprite = WorkSprite;
            new_button.GetComponent<Button>().onClick.AddListener(()=> { ApplyChoose(WorkSprite.name); });
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ApplyChoose(string name)
    {
        Debug.Log("Chosen one: "+name);
    }
}
