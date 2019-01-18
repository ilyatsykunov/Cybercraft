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

    //Stats
    public int totalStrength;
    public int totalAiming;
    public int totalSpeed;
    protected int initialStrength;
    protected int initialAiming;
    protected int initialSpeed;
    public List<Modification> listOfMods = new List<Modification>();

    //Figthing
    public bool meleeAttack;
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

    public bool patrolMode;
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        RemoveTarget();
        oldTarget = target;
        meleeAttack = false;
    }
    // Update is called once per frame
    protected override void Update()
    {
        if(isAlive == true)
        {
            //UpdateStats();
            base.Update();
            ChangeAnimation();
            Patrol();
            if ((target != transform.position && isMoving == false) || oldTarget != target)
            {
                oldTarget = target;
                Move();
            }
            if (attackTarget != null && isAttacking == false)
            {
                if (weapon.GetComponent<Gun>().canShoot == false)
                {
                    
                }
                Attack();
            }
            //Remove target if its dead
            if (attackTarget != null && attackTarget.GetComponent<Human>().isAlive == false)
            {
                RemoveTarget();
            }
            if (targetBuilding != null)
            {
                CaptureBuilding();
            }
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
    protected void Patrol()
    {
        if(patrolPoint1 != null && patrolPoint2 != null)
        {
            if (patrolPoint1.activeInHierarchy == true && patrolPoint2.activeInHierarchy == true)
            {
                if (patrolMode == false)
                {
                    target = patrolPoint1.transform.position;
                }
                if (transform.position == patrolPoint1.transform.position)
                {
                    patrolMode = true;
                    target = patrolPoint2.transform.position;
                }
                if (transform.position == patrolPoint2.transform.position)
                {
                    patrolMode = true;
                    target = patrolPoint1.transform.position;
                }
            }
        }
        else
        {
            patrolPoint1 = new GameObject();
            patrolPoint2 = new GameObject();
            patrolPoint1.SetActive(false);
            patrolPoint2.SetActive(false);
        }

    }
    protected void Attack()
    {
        distanceToEnemy = Vector3.Distance(gameObject.transform.position, attackTarget.transform.position);
        directionToEnemy = (attackTarget.transform.position - transform.position).normalized;
        gameObject.transform.LookAt(attackTarget.transform.position);
        if (!Physics.Raycast(gameObject.transform.position, directionToEnemy, distanceToEnemy, ignoreMask) && (!Physics.Raycast(shootFrom.transform.position, directionToEnemy, distanceToEnemy, ignoreMask)) && distanceToEnemy < 15f)
        {
            enemyInRange = true;
        }
        else
        {
            enemyInRange = false;
        }
        if (enemyInRange == true)
        {
            target = transform.position;
            shouldBeAttacking = true;
            if (distanceToEnemy < 3f && isAttacking == false && (weapon == null || weapon.GetComponent<Gun>().canShoot == false || meleeAttack == true))
            {
                StartCoroutine("Melee");
            }
            else if (distanceToEnemy > 3f && isAttacking == false && weapon != null && meleeAttack == false)
            {
                oldAttackTarget = attackTarget;
                weapon.GetComponent<Gun>().RangeAttack(totalAiming);
            }
        }
        else
        {
            shouldBeAttacking = false;
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
        attackTarget.GetComponent<Character>().health -= totalStrength * 2;
        isAttacking = false;
    }
    protected void CaptureBuilding() 
    {
        GameObject door = targetBuilding.GetComponent<Building>().doors;
        target = new Vector3(door.transform.position.x, transform.position.y, door.transform.position.z);
        float doorDistance = Vector3.Distance(transform.position, targetBuilding.GetComponent<Building>().doors.transform.position);
        if (doorDistance <= 1f)
        {
            EnterBuilding();
        }
        Building buildingScript = targetBuilding.GetComponent<Building>();
        if (isInsideBuilding == true)
        {
            buildingScript.StartCoroutine("Capture", faction);
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
    protected virtual void ChangeAnimation()
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
    
    //Stats
    private void UpdateStats()
    {
        if(listOfMods.Count > 0)
        {
            totalStrength = TotalStrength();
            totalAiming = TotalAiming();
            totalSpeed = TotalSpeed();
        }
        agent.speed = totalSpeed / 15;
    }
    private int TotalStrength()
    {
        int strength = initialStrength;
        foreach (Modification mod in listOfMods)
        {
            strength += mod.GetComponent<Modification>().strength;
        }
        return strength;
    }
    private int TotalAiming()
    {
        int aiming = initialAiming;
        foreach (Modification mod in listOfMods)
        {
            aiming += mod.GetComponent<Modification>().aiming;
        }
        return aiming;
    }
    private int TotalSpeed()
    {
        int speed = initialSpeed;
        foreach (Modification mod in listOfMods)
        {
            speed += mod.GetComponent<Modification>().speed;
        }
        return speed;
    }
}
