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

    [SerializeField]
    protected bool isMoving;
    protected NavMeshAgent agent;

    [SerializeField]
    public Vector3 target;
    public GameObject buildingToEnter;
    public bool isInsideBuilding;

    protected Animator charAnimator;

    public GameObject screen;

    // Use this for initialization
    protected virtual void Awake () {
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
            health = 0;
            Death();
        }
    }
    protected void Death()
    {
        StopAllCoroutines();
        isAlive = false;
        gameObject.SetActive(false);
    }
    protected void EnterBuilding()
    {
        if(buildingToEnter != null)
        {
            GameObject door = buildingToEnter.transform.Find("Door").gameObject;
            target = door.transform.position;
            if(transform.position == new Vector3(target.x, transform.position.y, target.z))
            {
                isInsideBuilding = true;
            }
            else
            {
                isInsideBuilding = false;
            }
        }
        if (isInsideBuilding)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

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
}
