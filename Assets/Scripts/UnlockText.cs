using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockText : MonoBehaviour {
    public AnimationCurve curve;
    public TMPro.TextMeshProUGUI text;
    public float exaggurationScalar = 10.0f;
    private void OnEnable() {
        IEnumerator Lerp() {
            float x = 0.0f;
            while(x < 1.0f) {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime;
                text.color = new Color(text.color.r,text.color.g,text.color.b,1.0f - curve.Evaluate(x));
                gameObject.transform.position = gameObject.transform.position + new Vector3(0.0f,exaggurationScalar*curve.Evaluate(x),0.0f);
            }
            gameObject.SetActive(false);
        }
        StartCoroutine(Lerp());
    }
}
