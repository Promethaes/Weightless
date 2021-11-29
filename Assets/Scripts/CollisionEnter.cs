using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEnter : MonoBehaviour
{
     public string tag = "null";
    public UnityEvent withinRangeEvent;
    public UnityEvent exitRangeEvent;

    private void OnCollisionEnter2D(Collision2D other) {
        if(tag == "null" || other.gameObject.CompareTag(tag))
            withinRangeEvent?.Invoke();
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(tag == "null" || other.gameObject.CompareTag(tag))
            exitRangeEvent?.Invoke();
    }
}
