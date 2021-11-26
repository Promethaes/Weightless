using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGrappleCharge : MonoBehaviour {
    public float respawnTime = 2.0f;
    public SpriteRenderer sprite;
    bool _respawning = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(_respawning)
            return;
        var p = other.gameObject.GetComponent<PlayerControls>();
        if(!p)
            return;
        p.AddGrappleCharge();
        StartCoroutine(ChargeRespawn());
    }

    IEnumerator ChargeRespawn(){
        sprite.color = sprite.color - new Color(0.0f,0.0f,0.0f,0.5f);
        _respawning = true;
        yield return new WaitForSeconds(respawnTime);
        sprite.color = sprite.color + new Color(0.0f,0.0f,0.0f,0.5f);
        _respawning = false;
    }
}
