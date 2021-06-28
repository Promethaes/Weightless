using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosLerp : MonoBehaviour {
    [SerializeField] AnimationCurve lerpCurve;

    IEnumerator Lerp(Vector2 newPos) {
        float x = 0.0f;
        var pos = gameObject.transform.position;
        while(x < 1.0f) {
            yield return new WaitForEndOfFrame();
            x += Time.deltaTime;
            var pVec = Vector2.Lerp(pos, newPos, lerpCurve.Evaluate(x));
            gameObject.transform.position = new Vector3(pVec.x, pVec.y, -10.0f);
        }
    }
    public void LerpPosition(Vector2 newPos) {
        StopCoroutine(Lerp(newPos));
        StartCoroutine(Lerp(newPos));
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
            LerpPosition(other.gameObject.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
            StopCoroutine(Lerp(Vector3.zero));

    }
}
