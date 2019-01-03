using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : BaseBuilding
{
    public GameObject unit1;

    public float spottingDistance;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        SpotEnemies();
    }
    protected void SpotEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject enemy in enemies)
        {
            foreach(GameObject hero in heroes)
            {
                if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) <= spottingDistance || hero.GetComponent<Hero>().spottingDistance < Vector3.Distance(hero.transform.position, enemy.transform.position))
                {
                    enemy.GetComponent<Enemy>().canBeSeen = true;
                }
                else
                {
                    enemy.GetComponent<Enemy>().canBeSeen = false;
                }
            }
        }
    }
}
