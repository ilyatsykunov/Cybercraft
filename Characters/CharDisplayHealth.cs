using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDisplayHealth : MonoBehaviour {

    private float health;
    private Color baseColor;
	// Use this for initialization
	void Start () {
        baseColor = GetComponent<SkinnedMeshRenderer>().materials[1].color;
	}

    void Update()
    {
        health = GetComponentInParent<Human>().health;

        SkinnedMeshRenderer renderer = GetComponentInParent<SkinnedMeshRenderer>();
        Material mat = renderer.materials[1];
        float emission = 10f;
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission) * (health/100f);
        Color mainColor = baseColor * (health / 100f);
        mat.SetColor("_EmissionColor", finalColor);
        mat.SetColor("_Color", mainColor);
    }
}
