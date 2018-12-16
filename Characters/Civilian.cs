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
    protected override void Awake () {
        base.Awake();
    }
	
	// Update is called once per frame
	protected override void Update () {

        base.Update();
        ChangeAnimation();
        yAxis = transform.position.y;
        if (transform.position != target && isMoving == false)
        {
            target = transform.position;
        }
        else if (transform.position == target && isMoving == false)
        {
            RandomTarget();
            agent.SetDestination(target);
        }
	}
    private void RandomTarget()
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
        target = buildings[randomBuilding].transform.position;

    }

    protected void ChangeAnimation()
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
