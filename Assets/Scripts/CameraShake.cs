using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float initialAmplitude = 4.0f;
    public float initialFrequency = 1.0f;

    public AnimationCurve decayCurve;
    public float decaySpeed = 1.0f;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;


    bool shaking = false;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player") || shaking)
            return;
        IEnumerator Lerp() {
            var noise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = initialAmplitude;
            noise.m_FrequencyGain = initialFrequency;

            shaking = true;
            float x = 0.0f;
            while(x < 1.0f) {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime * decaySpeed;
                noise.m_FrequencyGain = Mathf.Lerp(initialFrequency, 0.0f, decayCurve.Evaluate(x));
                noise.m_AmplitudeGain = Mathf.Lerp(initialAmplitude, 0.0f, decayCurve.Evaluate(x));
            }
            gameObject.SetActive(false);
        }
        StartCoroutine(Lerp());
    }

}
