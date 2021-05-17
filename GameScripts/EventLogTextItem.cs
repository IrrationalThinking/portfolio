using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*EventLogTextItem script controls a single text box which is inside the event box*/
public class EventLogTextItem : MonoBehaviour {
    public void setText(string eventText) {
        GetComponent<Text>().text = eventText;
    }
}
