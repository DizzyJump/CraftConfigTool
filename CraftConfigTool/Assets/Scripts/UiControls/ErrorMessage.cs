using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class ErrorMessage : ScriptableObject {
    public string Title;
    public string Message;
    public EventObject ShowMessageEvent;

    public void Show(string title, string message)
    {
        Title = title;
        Message = message;
        ShowMessageEvent.Invoke();
    }
}
