/// <summary>
/// AI for non-playabale humanoid GameObjects.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Civilian : Human {

    private float yAxis;

    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {

        base.Update();
        ChangeAnimation();
        yAxis = transform.position.y;
        if (transform.position != target && isMoving == false && isInsideBuilding == false)
        {
            Move();
        }
        if(isMoving == false && isInsideBuilding == false)
        {
            if (targetBuilding != null)
            {
                float doorDistance = Vector3.Distance(transform.position, targetBuilding.GetComponent<Building>().doors.transform.position);
                if(doorDistance <= 1f)
                {
                    EnterBuilding();
                }
            }
            else
            {
                RandomTarget();
            }
        }
	}
    protected void RandomTarget()
    {
        int randomThingToDo = Random.Range(0, 10);
        if (randomThingToDo == 1)
        {
            LookForBuildings();

        }
        else
        {
            target = new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), yAxis, Random.Range(transform.position.z - 2, transform.position.z + 2));
        }
    }

    protected void LookForBuildings() //Replacement for trigger colliders
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        int randomBuilding = Random.Range(0, buildings.Length);
        targetBuilding = buildings[randomBuilding];
        GameObject door = targetBuilding.GetComponent<Building>().doors;
        target = new Vector3(door.transform.position.x, transform.position.y, door.transform.position.z);
    }

    protected virtual void ChangeAnimation()
    {
        if (isMoving == true)
        {
            charAnimator.SetBool("isWalking", true);
            charAnimator.SetBool("isIdle", false);
        }
        else if (isMoving == false)
        {
            charAnimator.SetBool("isWalking", false);
            charAnimator.SetBool("isIdle", true);

        }
    }
}
