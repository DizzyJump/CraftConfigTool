using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePanel : MonoBehaviour {
    public ErrorMessage msg;
    public TextMeshProUGUI title;
    public TextMeshProUGUI message;

    private void OnEnable()
    {
        title.text = msg.Title;
        message.text = msg.Message;
    }
}
