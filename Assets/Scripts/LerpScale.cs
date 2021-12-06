using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScale : MonoBehaviour {
    public float scalar = 10.0f;
    Vector3 newScale;
    public AnimationCurve punchCurve;
    public float lerpSpeed;

    Vector3 _originalScale;
    // Start is called before the first frame update
    void Start() {
        _originalScale = transform.localScale;
        newScale = _originalScale + (_originalScale / scalar);
        IEnumerator Lerp() {
            while(true) {
                yield return null;
                float x = 0.0f;
                while(x < 1.0f) {
                    yield return new WaitForEndOfFrame();
                    x += Time.deltaTime * lerpSpeed;
                    transform.localScale = Vector3.Lerp(_originalScale, newScale, punchCurve.Evaluate(x));
                }
                x = 1.0f;
                while(x > 0.0f) {
                    yield return new WaitForEndOfFrame();
                    x -= Time.deltaTime * lerpSpeed;
                    transform.localScale = Vector3.Lerp(_originalScale, newScale, punchCurve.Evaluate(x));
                }
                x = 0.0f;
            }
        }
        StartCoroutine(Lerp());
    }
}
