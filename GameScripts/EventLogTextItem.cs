using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLogTextItem : MonoBehaviour {
    public void setText(string eventText) {
        GetComponent<Text>().text = eventText;
    }
}
