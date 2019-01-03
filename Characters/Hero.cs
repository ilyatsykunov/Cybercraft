/// <summary>
/// Controller for playable humanoid GameObjects.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    protected bool isSelected;
    public float spottingDistance;
    public PlayerController pc;

    // Use this for initialization
    protected override void Start()
    {
        pc = GameObject.Find("World").GetComponent<PlayerController>();
        isSelected = false;
        base.Start();
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
        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftControl))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
            {
                if(pc.isSelectingPatrol == false)
                {
                    patrolMode = false;
                    patrolPoint1.SetActive(false);
                    patrolPoint2.SetActive(false);
                    if (rayHit.collider.gameObject.tag == "Enemy")
                    {
                        attackTarget = rayHit.collider.gameObject;
                    }
                    else if (rayHit.collider.gameObject.tag == "Building" && pc.isSelectingCapture  == true)
                    {
                        targetBuilding = rayHit.collider.gameObject;
                    }
                    else
                    {
                        attackTarget = null;
                        target = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                    }
                }
                else
                {
                    if(patrolPoint1.activeInHierarchy == false || patrolPoint2.activeInHierarchy == true)
                    {
                        patrolPoint1.transform.position = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                        patrolPoint1.SetActive(true);
                        patrolPoint2.SetActive(false);
                    }
                    else if(patrolPoint1.activeInHierarchy == true && patrolPoint2.activeInHierarchy == false)
                    {
                        patrolPoint2.transform.position = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                        patrolPoint2.SetActive(true);
                        pc.isSelectingPatrol = false;
                    }
                }

            }
        }
    }
    public void Stop()
    {
        target = transform.position;
        RemoveTarget();
    }
}
