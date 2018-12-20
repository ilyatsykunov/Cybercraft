/// <summary>
/// Branch of humanoid class. Base class for all playable humanoid GameObjects.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : Human
{
    public string faction;
    public Vector3 oldTarget;

    //Figthing
    public GameObject weapon;
    public bool isAttacking;
    public bool shouldBeAttacking;
    public bool isReloading;
    public GameObject attackTarget;
    [HideInInspector]
    public GameObject oldAttackTarget;
    [SerializeField]
    protected bool enemyInRange;
    [SerializeField]
    protected float distanceToEnemy;
    protected Vector3 directionToEnemy;
    public GameObject shootFrom;
    public LayerMask ignoreMask;

    //MiniMap
    public GameObject miniMapPoint;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        oldTarget = target;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CaptureBuilding();
        ChangeAnimation();
        if ((target != transform.position && isMoving == false) || oldTarget != target)
        {
            oldTarget = target;
            Move();
        }
        if (attackTarget != null && isAttacking == false)
        {
            if (weapon.GetComponent<Gun>().canShoot == false)
            {
                TakeCover();
            }
            Attack();
        }
        if (attackTarget != null && attackTarget.GetComponent<Human>().isAlive == false && isAttacking == true)
        {
            RemoveTarget();
        }
    }
    protected void TakeCover()
    {
        NavMeshHit hit;
        if (NavMesh.FindClosestEdge(transform.position, out hit, NavMesh.AllAreas))
        {
            target = hit.normal;
            /*if (Vector3.Dot(hit.normal, (attackTarget.transform.position - hit.position)) < 0)
            {
                target = hit.position;
            }*/
        }
    }

    protected void Move()
    {
        if (isAttacking == true)
        {
            RemoveTarget();
        }
        agent.SetDestination(target);
    }
    protected void Attack()
    {
        shouldBeAttacking = true;
        target = transform.position;
        distanceToEnemy = Vector3.Distance(gameObject.transform.position, attackTarget.transform.position);
        directionToEnemy = (attackTarget.transform.position - transform.position).normalized;
        gameObject.transform.LookAt(attackTarget.transform.position);
        if (!Physics.Raycast(gameObject.transform.position, directionToEnemy, distanceToEnemy, ignoreMask) && (!Physics.Raycast(shootFrom.transform.position, directionToEnemy, distanceToEnemy, ignoreMask)))
        {
            enemyInRange = true;
        }
        else
        {
            enemyInRange = false;
        }
        if (enemyInRange == true)
        {
            if (distanceToEnemy < 3f && isAttacking == false)
            {
                StartCoroutine("Melee");
            }
            else if (distanceToEnemy > 3f && distanceToEnemy < 15f && isAttacking == false && weapon != null)
            {
                oldAttackTarget = attackTarget;
                weapon.GetComponent<Gun>().RangeAttack();
            }
        }
        else
        {
            target = new Vector3(attackTarget.transform.position.x, transform.position.y, attackTarget.transform.position.z);
        }
    }
    protected IEnumerator Melee() //Hit attackTarget
    {
        Debug.Log("Started MELEE attack");
        isAttacking = true;
        target = transform.position;
        weapon.GetComponent<Gun>().StopCoroutine("Shoot");
        yield return new WaitForSeconds(1);
        attackTarget.GetComponent<Character>().health -= 10;
        isAttacking = false;
    }
    protected void CaptureBuilding() 
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject building in buildings)
        {
            Building buildingScript = building.GetComponent<Building>();
            float distance = Vector3.Distance(gameObject.transform.position, building.transform.position);
            if (buildingScript.faction != faction && distance < 10f)
            {
                buildingScript.StartCoroutine(buildingScript.Capture(faction));
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 10;
            if (attackTarget == null && isMoving == false)
            {
                attackTarget = collision.gameObject.GetComponent<Bullet>().launchedBy;
            }
        }
    }
    protected void RemoveTarget()
    {
        StopCoroutine("Melee");
        weapon.GetComponent<Gun>().StopCoroutine("Shoot");
        distanceToEnemy = 0f;
        enemyInRange = false;
        isAttacking = false;
        attackTarget = null;
        oldAttackTarget = null;
        shouldBeAttacking = false;
    }

    protected void ChangeAnimation()
    {
        if(shouldBeAttacking == true)
        {
            charAnimator.SetBool("isIdle", false);
            charAnimator.SetBool("isWalking", false);
            charAnimator.SetBool("isRangeAttacking", true);
        }
        else if(shouldBeAttacking == false)
        {
            charAnimator.SetBool("isRangeAttacking", false);
            if (isMoving == true)
            {
                charAnimator.SetBool("isWalking", true);
                charAnimator.SetBool("isIdle", false);
                charAnimator.SetBool("isRangeAttacking", false);
            }
            else if (isMoving == false)
            {
                charAnimator.SetBool("isWalking", false);
                charAnimator.SetBool("isIdle", true);
                charAnimator.SetBool("isRangeAttacking", false);

            }
        }
    }
}
