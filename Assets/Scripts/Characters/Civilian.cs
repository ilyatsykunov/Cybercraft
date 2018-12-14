using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Civilian : MonoBehaviour {

    protected float speed;

    [SerializeField]
    protected bool isMoving;
    protected NavMeshAgent agent;

    [SerializeField]
    protected Vector3 target;
    private float yAxis;

    protected Animator charAnimator;

    // Use this for initialization
    void Awake () {
 
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = transform.position;
        charAnimator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("GetSpeed");
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
            lookForBuildings();

        }
        else
        {
            target = new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), yAxis, Random.Range(transform.position.z - 2, transform.position.z + 2));
        }
    }

    protected void lookForBuildings() //Replacement for trigger colliders
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        int randomBuilding = Random.Range(0, buildings.Length);
        target = buildings[randomBuilding].transform.position;

    }
    protected IEnumerator GetSpeed()
    {
        Vector3 pos1 = transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 pos2 = transform.position;
        if (pos1 != pos2)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
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
