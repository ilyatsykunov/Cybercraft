/// <summary>
/// Controller for playable humanoid GameObjects.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    protected bool isSelected;

    // Use this for initialization
    protected override void Awake()
    {
        screen = canvas.transform.Find("UnitScreen").gameObject;
        isSelected = false;
        base.Awake();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (gameObject.GetComponent<ClickOn>().currentlySelected == true)
        {
            GetDirection();
        }
    }
    protected void GetDirection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
            {
                if (rayHit.collider.gameObject.tag == "Enemy")
                {
                    attackTarget = rayHit.collider.gameObject;
                }
                else if (rayHit.collider.gameObject.tag == "Building")
                {
                    buildingToEnter = rayHit.collider.gameObject;
                }
                else
                {
                    attackTarget = null;
                    target = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                }
            }
        }
    }

}
