using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour {

    public Vector3 mousePosition;

 
	void Update () {
        mousePosition = Camera.main.ViewportToWorldPoint(Input.mousePosition);
    }
}
