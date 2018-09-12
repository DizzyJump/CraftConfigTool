using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour {
    public EventObject dispatcher;
    public UnityEvent Trigger;
	// Use this for initialization
	void OnEnable () {
        dispatcher.AddHandler(OnFireEvent);
	}

    private void OnDisable()
    {
        dispatcher.RemoveHandler(OnFireEvent);
    }

    void OnFireEvent()
    {
        Trigger.Invoke();
    }
}
