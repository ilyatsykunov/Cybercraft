using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{

    [SerializeField]
    protected float speed;

    protected Vector2 direction;
    protected NavMeshAgent agent;

    [SerializeField]
    protected Vector3 target;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected bool isAttacking;
    [SerializeField]
    protected GameObject attackTarget;
    [SerializeField]
    protected float distanceToEnemy;

    protected virtual void Start()
    {
        health = 100;
        target = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        ManageHealth();
        if (target != transform.position)
        {
            Move();
        }
        if(attackTarget != null)
        {
            distanceToEnemy = Vector3.Distance(gameObject.transform.position, attackTarget.transform.position);
            if (isAttacking == false)
            {
                StartCoroutine(Attack());
            }
            
        }


    }

    protected void Move()
    {
        agent.SetDestination(target);
    }

    protected void ManageHealth()
    {
        if(health > 100)
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
        gameObject.SetActive(false);
    }
    protected IEnumerator Attack()
    {
        if(distanceToEnemy < 5f)
        {
            isAttacking = true;
            yield return new WaitForSeconds(1);
            attackTarget.GetComponent<Character>().health -= 10;
            isAttacking = false;
        }
        else
        {
            target = attackTarget.transform.position;
        }
    }




}
