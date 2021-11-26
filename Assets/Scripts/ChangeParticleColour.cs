using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParticleColour : MonoBehaviour {
    public Color newColour;
    public ParticleSystem particleSystem;

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player"))
            return;
        ParticleSystem.MainModule settings = particleSystem.main;
        settings.startColor = newColour;
    }

}
