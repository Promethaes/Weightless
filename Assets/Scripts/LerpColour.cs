using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LerpColour : MonoBehaviour {
    public AnimationCurve animationCurve;
    public float lerpSpeed = 1.0f;
    public Color startColour;
    public Color endColour;

    public SpriteShapeRenderer sprite;

    public void StartLerp() {
        IEnumerator Lerp() {
            float x = 0.0f;
            while(x < 1.0f) {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime * lerpSpeed;
                sprite.color = Color.Lerp(startColour, endColour, animationCurve.Evaluate(x));
            }
        }
        StartCoroutine(Lerp());
    }
}
