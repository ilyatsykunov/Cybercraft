/// <summary>
/// Base class for all humanoid GameObjects.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Human : MonoBehaviour {

    protected float speed;
    public int health;
    public bool isAlive;

    public bool isMoving;
    protected NavMeshAgent agent;

    public Vector3 target;
    public GameObject targetBuilding;
    public bool isInsideBuilding;

    protected Animator charAnimator;

    public GameObject screen;
    public string nameToDisplay;
    public string infoToDisplay;

    // Use this for initialization
    protected virtual void Start() {
        isAlive = true;
        health = 100;
        target = transform.position;
        isMoving = false;
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = transform.position;
        charAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update () {
        ManageHealth();
        StartCoroutine("GetSpeed");
    }
    protected void ManageHealth()
    {
        if (health > 100)
        {
            health = 100;
        }
        if (health <= 0)
        {
            if(isAlive == true)
            {
                health = 0;
                StopAllCoroutines();
                charAnimator.SetBool("isIdle", false);
                charAnimator.SetBool("isWalking", false);
                charAnimator.SetBool("isRangeAttacking", false);
                charAnimator.SetBool("isDead", true);
                StartCoroutine("Death");
                isAlive = false;
            }
        }
    }
    protected void Move()
    {
        agent.SetDestination(target);
    }
    protected void EnterBuilding()
    {
        targetBuilding.GetComponent<Building>().unitsInside.Add(gameObject);
        isInsideBuilding = true;
        gameObject.SetActive(false);
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
    protected IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
