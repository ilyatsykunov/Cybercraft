using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLights : MonoBehaviour {

    private Light go;

	// Use this for initialization
	void Start () {
        go = gameObject.GetComponent<Light>();
        StartCoroutine("Blink");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator Blink()
    {
        go.enabled = false;
        float sec = Random.Range(5f, 60f);
        yield return new WaitForSeconds(sec);
        go.enabled = true;
        yield return new WaitForSeconds(sec);
        StartCoroutine("Blink");
    }
}
