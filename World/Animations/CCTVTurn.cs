using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVTurn : MonoBehaviour {

    public GameObject turnPoint;
    private float seconds;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        seconds += Time.deltaTime;
        if(seconds < 5f)
        {
            transform.RotateAround(turnPoint.transform.position, Vector3.up, 10f * Time.deltaTime);
        }
        else if(seconds > 5f && seconds < 10f)
        {
            transform.RotateAround(turnPoint.transform.position, -Vector3.up, 10f * Time.deltaTime);
        }
        else
        {
            seconds = 0f;
        }
        
    }
}
