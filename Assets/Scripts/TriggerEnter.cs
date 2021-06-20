using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour
{
    public UnityEvent withinRangeEvent;
    public UnityEvent exitRangeEvent;

    private void OnTriggerEnter(Collider other) {
        withinRangeEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other) {
        exitRangeEvent?.Invoke();
    }
}
