using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAgent : MonoBehaviour {
    public AnimationCurve animationCurve;
    public float lerpSpeed;
    public Transform lerpPoint;


    Vector3 _orignalPos = Vector3.zero;
    // Start is called before the first frame update
    void Start() {
        _orignalPos = transform.position;
    }

    public void StartLerp() {
        IEnumerator Lerp() {
            float x = 0.0f;
            while(x < 1.0f) {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime * lerpSpeed;

                transform.position = Vector3.Lerp(_orignalPos, lerpPoint.position, animationCurve.Evaluate(x));
            }
        }
        StartCoroutine(Lerp());
    }
}
