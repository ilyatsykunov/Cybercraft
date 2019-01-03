using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour {

    public Vector3 downPos;
    public Vector3 upPos;
    public Vector3 target;

	void Update () {
        if(transform.localPosition != target)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 0.005f * Time.deltaTime);
        }
	}
}
