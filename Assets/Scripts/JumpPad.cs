using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
    public AnimationCurve curve;
    public float exaggurationScalar = 1.5f;
    Vector3 originalScale = Vector3.one;
    // Start is called before the first frame update
    void Start() {
        originalScale = transform.localScale;
    }

    public void Animate() {
        StopCoroutine(Lerp());
        StartCoroutine(Lerp());
    }

    IEnumerator Lerp() {
        float x = 0.0f;
        while(x < 1.0f) {
            yield return new WaitForEndOfFrame();
            x += Time.deltaTime;
            var xVal = originalScale.x * curve.Evaluate(x);
            var yVal = exaggurationScalar * originalScale.y * curve.Evaluate(x);
            var newScale = new Vector3(originalScale.x - xVal, originalScale.y + yVal, originalScale.z);
            transform.localScale = newScale;
        }
    }
}
