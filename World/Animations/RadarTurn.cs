using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTurn : MonoBehaviour {

    public GameObject pole;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(pole.transform.position, Vector3.up, 5f * Time.deltaTime);
	}
}
