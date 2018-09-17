using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class ChooseIconInfo : ScriptableObject {
    public string DefaultName;
    public ChooseIconPanel.ApplyChooseDelegate Callback;
    public EventObject Event;

    public void Invoke(string default_name, ChooseIconPanel.ApplyChooseDelegate callback)
    {
        DefaultName = default_name;
        Callback = callback;
        Event.Invoke();
    }
}
