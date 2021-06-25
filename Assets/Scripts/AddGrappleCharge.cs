using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGrappleCharge : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        var p = other.gameObject.GetComponent<PlayerControls>();
        if(!p)
            return;
        p.AddGrappleCharge();
        gameObject.SetActive(false);
    }
}
