using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    //Ammo
    protected int magazineTotal;
    [SerializeField]
    protected int magazineCurrent;
    protected int totalCapacity;
    [SerializeField]
    protected int currentTotal;
    //Stats
    protected float firingRate;
    protected float maxDamage;
    protected float reloadTime;

    public bool canShoot;
    public GameObject parentCharacter;
    [SerializeField]
    protected GameObject shootFrom;
    [SerializeField]
    protected GameObject bulletPrefab;

    protected virtual void Update()
    {
        if(parentCharacter == null) 
        {
            if(gameObject.transform.parent.gameObject != null)
            {
                parentCharacter = gameObject.transform.parent.gameObject;
                parentCharacter.GetComponent<Character>().weapon = gameObject;
            }
            
        }
        if(shootFrom == null)
        {
            shootFrom = gameObject.transform.GetChild(0).gameObject;
        }
        
    }

    public void RangeAttack()
    {
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
            Vector3 shootTo = new Vector3(Random.Range(attackTarget.transform.position.x - 0.5f, attackTarget.transform.position.x + 0.5f), Random.Range(attackTarget.transform.position.y - 1f, attackTarget.transform.position.y + 1f), Random.Range(attackTarget.transform.position.z - 0.5f, attackTarget.transform.position.z + 0.5f));
            GameObject spawnedBullet = Instantiate(bulletPrefab, shootFrom.transform.position, shootFrom.transform.rotation);
            spawnedBullet.GetComponent<Bullet>().ShootTo(shootTo);
            spawnedBullet.GetComponent<Bullet>().launchedBy = parentCharacter;
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
}
