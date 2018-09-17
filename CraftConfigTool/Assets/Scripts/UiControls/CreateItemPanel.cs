using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateItemPanel : MonoBehaviour {
    public CraftEngine engine;
    public TMP_InputField input;
    public string IconName = "";
    public Button button;
    public ChooseIconInfo info;
    public IconSet Icons;
    public Image Icon;
    public EventObject OnAdd;
    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        input.text = "";
        IconName = "";
    }

    // Update is called once per frame
    void Update () {
        button.interactable = input.text != "" && !engine.ContainsItem(input.text);
    }

    public void OnCreate()
    {
        var item = engine.AddItem(input.text);
        item.IconName = IconName;
        gameObject.SetActive(false);
        OnAdd.Invoke();
    }

    public void OnClickIcon()
    {
        info.Invoke(IconName, OnSwitchIcon);
    }

    public void OnSwitchIcon(string name)
    {
        IconName = name;
        Icon.sprite = Icons.Get(IconName);
    }
}
