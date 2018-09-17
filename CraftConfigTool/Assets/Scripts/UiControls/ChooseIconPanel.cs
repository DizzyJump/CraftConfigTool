using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseIconPanel : MonoBehaviour {
    public delegate void ApplyChooseDelegate(string name);
    public IconSet Icons;
    public Transform ChooseButtonPrefab;
    public Transform GridTransform;
    public GameObject WindowRoot;
    string ChosenSpriteName;
    public ChooseIconInfo info;
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

    private void OnEnable()
    {
        ChosenSpriteName = info.DefaultName;
    }

    private void OnDisable()
    {
        if(info.Callback != null)
            info.Callback(ChosenSpriteName);
        else
            Debug.LogError("Callback doent set in ChooseIconPanel!");
    }

    void ApplyChoose(string name)
    {
        ChosenSpriteName = name;
        WindowRoot.SetActive(false);
    }
}
