using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerStay : MonoBehaviour {
    public UnityEvent withinRangeEvent;
    public UnityEvent exitRangeEvent;

    private void OnTriggerStay2D(Collider2D other) {
        withinRangeEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other) {
        exitRangeEvent?.Invoke();
    }
}
