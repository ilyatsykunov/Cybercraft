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

    protected Vector2 direction;
    public Vector3 oldTarget;

    //Figthing
    public GameObject weapon;
    public bool isAttacking;
    public bool isReloading;
    public GameObject attackTarget;
    public GameObject oldAttackTarget;
    [SerializeField]
    protected bool enemyInRange;
    [SerializeField]
    protected float distanceToEnemy;
    protected Vector3 directionToEnemy;
    [SerializeField]
    protected GameObject shootFrom;
    [SerializeField]
    protected GameObject bullet;
    public LayerMask ignoreMask;

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
        ManageHealth();
        ChangeAnimation();
        if ((target != transform.position && isMoving == false) || oldTarget != target)
        {
            oldTarget = target;
            Move();
        }
        if (attackTarget != null && isAttacking == false)
        {
            Attack();
        }
        else if (attackTarget != null && attackTarget.GetComponent<Human>().isAlive == false && isAttacking == true)
        {
            StopCoroutine("Melee");
            weapon.GetComponent<Gun>().StopCoroutine("Shoot");
            distanceToEnemy = 0f;
            enemyInRange = false;
            isAttacking = false;
            attackTarget = null;
            oldAttackTarget = null;
        }
    }

    protected void Move()
    {

        if (isAttacking == true)
        {
            StopCoroutine("Melee");
            weapon.GetComponent<Gun>().StopCoroutine("Shoot");
            distanceToEnemy = 0f;
            enemyInRange = false;
            isAttacking = false;
        }
        agent.SetDestination(target);
    }
    protected void Attack()
    {
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
            BuildingClass buildingClass = building.GetComponent<BuildingClass>();
            float distance = Vector3.Distance(gameObject.transform.position, building.transform.position);
            if (buildingClass.faction != faction && distance < 10f)
            {
                buildingClass.StartCoroutine(buildingClass.Capture(faction));
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") //Take bullet damage
        {
            Debug.Log(gameObject.name + " " + health);
            health -= 10;
            if (attackTarget == null && isMoving == false) //Assign shooter as the enemy
            {
                attackTarget = collision.gameObject.GetComponent<Bullet>().launchedBy;
            }
        }
    }

    protected void ChangeAnimation()
    {
        if (isMoving == true && isAttacking == false)
        {
            charAnimator.SetBool("isWalking", true);
            charAnimator.SetBool("isIdle", false);
        }
        else if (isMoving == false && isAttacking == false)
        {
            charAnimator.SetBool("isWalking", false);
            charAnimator.SetBool("isIdle", true);

        }
        else if(isMoving == false && isAttacking == true)
        {
            charAnimator.SetBool("isWalking", false);
            charAnimator.SetBool("isIdle", true);
        }
    }
}
