using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour {
    public string tag = "null";
    public UnityEvent withinRangeEvent;
    public UnityEvent exitRangeEvent;

    private void OnTriggerEnter2D(Collider2D other) {
        if(tag == "null" || other.gameObject.CompareTag(tag))
            withinRangeEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(tag == "null" || other.gameObject.CompareTag(tag))
            exitRangeEvent?.Invoke();
    }
}
