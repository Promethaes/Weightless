using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public GameObject cameraAnchor;
    public CameraPosLerp camera;

    private void OnEnable() {
        camera.LerpPosition(new Vector2(cameraAnchor.transform.position.x,cameraAnchor.transform.position.y));
    }
}
