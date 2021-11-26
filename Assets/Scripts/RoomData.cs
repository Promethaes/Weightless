using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour {
    public GameObject cameraAnchor;
    public CameraPosLerp camera;

    [Tooltip("Not entirely sure how I want to use this yet. Probably for respawning and saving.")]
    public Transform RoomStartPoint;
    private void OnEnable() {
        camera.LerpPosition(new Vector2(cameraAnchor.transform.position.x, cameraAnchor.transform.position.y));
    }

    bool _disabling = false;
    public void DisableWhenPossible() {
        if(_disabling)
            return;
        IEnumerator WaitToDisable() {
            while(_disabling) {
                yield return new WaitForSeconds(0.25f);
                if(!camera.isLerping()) {
                    _disabling = false;
                    gameObject.SetActive(false);
                }
            }
        }
        _disabling = true;
        StartCoroutine(WaitToDisable());
    }
}
