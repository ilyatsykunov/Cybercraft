using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour {

    public Image healthBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        healthBar.transform.position = hpBarPosition;

	}
}
