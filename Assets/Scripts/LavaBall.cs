using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour {
    public Transform lerpPoint;
    public AnimationCurve punchCurve;
    public float lerpSpeed;

    Vector3 _orignalPosition;
    // Start is called before the first frame update
    void Start() {
        _orignalPosition = transform.localPosition;
        IEnumerator Lerp() {
            while(true) {
                yield return null;
                float x = 0.0f;
                while(x < 1.0f) {
                    yield return new WaitForEndOfFrame();
                    x += Time.deltaTime * lerpSpeed;
                    transform.localPosition = Vector3.Lerp(_orignalPosition, lerpPoint.localPosition, punchCurve.Evaluate(x));
                }
                x = 1.0f;
                while(x > 0.0f) {
                    yield return new WaitForEndOfFrame();
                    x -= Time.deltaTime * lerpSpeed;
                    transform.localPosition = Vector3.Lerp(_orignalPosition, lerpPoint.localPosition, punchCurve.Evaluate(x));
                }
                x = 0.0f;
            }
        }
        StartCoroutine(Lerp());
    }

}
