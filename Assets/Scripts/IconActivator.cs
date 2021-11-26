using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconActivator : MonoBehaviour {
    public List<GameObject> Icons;

    [HideInInspector] public float data;
    public float stepSize = 25.0f;

    private void Update() {
        float scaledData = data;
        foreach(var i in Icons)
            i.SetActive(false);

        int tempData = Mathf.CeilToInt((data / stepSize));
        tempData--;
        for(; tempData > -1; tempData--)
            Icons[tempData].SetActive(true);
    }
}
