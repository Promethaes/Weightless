using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenEffect : MonoBehaviour {
    public AnimationCurve curve;
    public Vector3 targetScale;
    public Vector3 originalScale;

    private void OnEnable() {
        IEnumerator Lerp() {
            float x = 0.0f;
            while(x < 1.0f) {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime;
                gameObject.transform.localScale = Vector3.Lerp(originalScale, targetScale, curve.Evaluate(x));
            }
            gameObject.SetActive(false);
        }
        StartCoroutine(Lerp());
    }
}
