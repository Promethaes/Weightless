using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timescale : MonoBehaviour {
    public float timescaleChange = 0.25f;
    public float changeDelay = 0.5f;
    void Start() {
        IEnumerator Change() {
            yield return new WaitForSeconds(changeDelay);

            Time.timeScale = timescaleChange;
            Time.fixedDeltaTime = timescaleChange * 0.02f;
        }
        StartCoroutine(Change());
    }

    public void ResetTimeScale() {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }

}
