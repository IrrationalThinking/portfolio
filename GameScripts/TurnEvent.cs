using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnEvent : MonoBehaviour {
    [SerializeField]
    private GameObject textTemplate;
    private List<GameObject> textItems;
    private void Start() {
        textItems = new List<GameObject>();
    }
    public void setText(string eventText) {
        if(textItems.Count == 20) {
            GameObject tempTextItem = textItems[0];
            Destroy(tempTextItem.gameObject);
            textItems.Remove(tempTextItem);
        }
        GameObject newText = Instantiate(textTemplate) as GameObject;
        newText.SetActive(true);

        newText.GetComponent<EventLogTextItem>().setText(eventText);
        newText.transform.SetParent(textTemplate.transform.parent, false);

        textItems.Add(newText.gameObject);
    }
}
