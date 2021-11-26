using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainOfLight : MonoBehaviour {
    public AnimationCurve lerpCurve;
    public SpriteRenderer spriteRenderer;
    public bool unlockDash = false;
    public bool unlockJetpack = false;
    public bool unlockGrapple = false;
    public float armorReduction = 0.25f;
    PlayerControls playerControls = null;
    bool _lerping = false;
    Vector3 originalScale = Vector3.one;

    private void Start() {
        originalScale = transform.localScale;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Player") || _lerping)
            return;

        if(playerControls == null)
            playerControls = other.gameObject.GetComponent<PlayerControls>();
        if(playerControls.interactPressed) {
            playerControls.LoseArmourPiece(armorReduction);
            _lerping = true;
            StartCoroutine(Lerp());
        }
    }


    IEnumerator Lerp() {
        if(unlockDash)
            playerControls.obtainedDash = true;
        if(unlockJetpack)
            playerControls.obtainedJetpack = true;
        if(unlockGrapple)
            playerControls.obtainedGrapple = true;

        float x = 0.0f;
        while(x < 1.0f) {
            yield return new WaitForEndOfFrame();
            x += Time.deltaTime;
            var xVal = originalScale.x * lerpCurve.Evaluate(x);
            var yVal = originalScale.y * lerpCurve.Evaluate(x);
            var alphaVal = 1.0f - lerpCurve.Evaluate(x);
            var newScale = new Vector3(originalScale.x - xVal, originalScale.y + yVal, originalScale.z);
            transform.localScale = newScale;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.g, alphaVal);
        }

        _lerping = false;
        gameObject.SetActive(false);
    }
}
