using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EventObject : ScriptableObject {
    public delegate void CustomEvent();
    event CustomEvent OnInvoke = ()=> { };
    public string Description;

    public void Invoke()
    {
        OnInvoke.Invoke();
    }

    public void AddHandler(CustomEvent func)
    {
        OnInvoke += func;
    }

    public void RemoveHandler(CustomEvent func)
    {
        OnInvoke -= func;
    }
}
