using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character {

    protected bool isSelected;

    // Use this for initialization
    protected override void Start () {
        isSelected = false;
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {

        if(gameObject.GetComponent<ClickOn>().currentlySelected == true)
        {
            GetDirection();
            base.Update();
        }
        

	}

    protected void GetDirection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
            {
                target = rayHit.point;
            }
        }
    }
}
