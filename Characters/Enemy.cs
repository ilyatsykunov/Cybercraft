using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {

    public bool isInAttackingState;
    public bool isInDefensiveState;

    protected Vector3 originalPosition;

    // Use this for initialization
    protected override void Awake()
    {

        base.Awake();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
        if(attackTarget == null && isMoving == false)
        {
            DefensiveState();
        }
        else
        {
            float distanceToOriginalPos = Vector3.Distance(gameObject.transform.position, originalPosition);
            if(distanceToOriginalPos > 15f)
            {
                attackTarget = null;
                target = originalPosition;
            }
        }
    }
    public void AttackingState()
    {

    }
    protected void DefensiveState()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject character in heroes)
        {
            float distance = Vector3.Distance(gameObject.transform.position, character.transform.position);
            if(distance < 10f)
            {
                originalPosition = transform.position;
                attackTarget = character;
                break;
            }
        }
    }

        /*
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        GameObject closestObstacle = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject building in buildings)
        {
            float distance = Vector3.Distance(gameObject.transform.position, building.transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestObstacle = building;
            }
        }
        */
}
