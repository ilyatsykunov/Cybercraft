using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    //Ammo
    public int magazineTotal;
    [SerializeField]
    public int magazineCurrent;
    public int totalCapacity;
    [SerializeField]
    public int currentTotal;
    //Stats
    protected float firingRate;
    protected float maxDamage;
    protected float reloadTime;

    public bool canShoot;
    public GameObject parentCharacter;
    [SerializeField]
    protected GameObject shootFrom;
    protected Light shootFromLight;
    [SerializeField]
    protected GameObject bulletPrefab;

    //Player
    public GameObject gunHolder;
    private int aiming;

    protected virtual void Awake()
    {
        AssignProperties();
    }

    protected virtual void Update()
    {
        transform.position = gunHolder.transform.position;
        transform.rotation = gunHolder.transform.rotation;
        if (parentCharacter == null) 
        {
            if(gameObject.transform.parent.gameObject != null)
            {
                parentCharacter = gameObject.transform.parent.gameObject;
                parentCharacter.GetComponent<Character>().weapon = gameObject;
                parentCharacter.GetComponent<Character>().shootFrom = shootFrom;
            }
        }
        AssignProperties();
    }

    protected void AssignProperties()
    {
        if (shootFrom == null)
        {
            shootFrom = gameObject.transform.Find("Shoot from").gameObject;
        }
        if (shootFromLight == null)
        {
            shootFromLight = shootFrom.GetComponent<Light>();
        }
        if (parentCharacter == null)
        {
            if (gameObject.transform.parent.gameObject != null)
            {
                parentCharacter = gameObject.transform.parent.gameObject;
                parentCharacter.GetComponent<Character>().weapon = gameObject;
                parentCharacter.GetComponent<Character>().shootFrom = shootFrom;
            }
        }
    }

    public void RangeAttack(int unitAiming)
    {
        aiming = unitAiming;
        if (magazineCurrent > 0)
        {
            canShoot = true;
            StartCoroutine("Shoot");
        }
        else if (magazineCurrent <= 0 && currentTotal > 0)
        {
            canShoot = true;
            StartCoroutine("Reload");
        }
        else
        {
            canShoot = false;
        }
    }

    public IEnumerator Shoot()
    {
        parentCharacter.GetComponent<Character>().isAttacking = true;
        magazineCurrent -= 1;
        parentCharacter.GetComponent<Character>().target = parentCharacter.transform.position;
        GameObject attackTarget = parentCharacter.GetComponent<Character>().attackTarget;
        GameObject oldAttackTarget = parentCharacter.GetComponent<Character>().oldAttackTarget;
        yield return new WaitForSeconds(firingRate);
        if (oldAttackTarget == attackTarget)
        {
            Vector3 shootTo = new Vector3(Random.Range(attackTarget.transform.position.x - aiming / 5f, attackTarget.transform.position.x + aiming / 5f), Random.Range(attackTarget.transform.position.y + 0.5f - aiming / 5f, attackTarget.transform.position.y + 0.5f + aiming / 5f), Random.Range(attackTarget.transform.position.z - aiming / 5f, attackTarget.transform.position.z + aiming / 5f));
            GameObject spawnedBullet = Instantiate(bulletPrefab, shootFrom.transform.position, Quaternion.identity) as GameObject;
            spawnedBullet.GetComponent<Bullet>().ShootTo(shootTo);
            spawnedBullet.GetComponent<Bullet>().launchedBy = parentCharacter;
            StartCoroutine("LightSwitch");
        }
        parentCharacter.GetComponent<Character>().isAttacking = false;
        StopCoroutine("Shoot");
    }
    protected IEnumerator Reload()
    {
        parentCharacter.GetComponent<Character>().isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        while(magazineCurrent < magazineTotal)
        {
            if(currentTotal > 0)
            {
                magazineCurrent += 1;
                currentTotal -= 1;
            }
            else
            {
                break;
            }
        }
        parentCharacter.GetComponent<Character>().isReloading = false;
        StopCoroutine("Reload");
    }
    protected IEnumerator LightSwitch()
    {
        shootFromLight.enabled = true;
        yield return new WaitForSeconds(0.15f);
        shootFromLight.enabled = false;
    }
}
