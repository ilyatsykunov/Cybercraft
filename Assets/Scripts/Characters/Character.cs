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

    protected virtual void Start()
    {
        target = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        
        if (target != transform.position)
        {
            Move();
        }
    }

    protected void Move()
    {
        agent.SetDestination(target);
    }

    
}
