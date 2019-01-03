using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuilding : Building {

    private bool isTraining;

    public List<GameObject> queue = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        isTraining = false;
    }

    protected override void Update()
    {
        base.Update();
        if (queue.Count > 0)
        {
            SpawnUnits();
        }
    }
    public void AddToQueue(GameObject unitToSpawn)
    {
        if (queue.Count < 5)
        {
            queue.Add(unitToSpawn);
        }
    }
    public void SpawnUnits()
    {
        foreach (GameObject unit in queue)
        {
            if (isTraining == false)
            {
                StartCoroutine("Training", unit);
            }
        }
    }
    public void CancelSpawn(GameObject unitToSpawn)
    {
        if (queue[0] == unitToSpawn)
        {
            StopCoroutine("Training");
        }
        else
        {
            queue.Remove(unitToSpawn);
        }
    }
    public IEnumerator Training(GameObject unit)
    {
        isTraining = true;
        yield return new WaitForSeconds(15f);
        Instantiate(unit, doors.transform.position, unit.transform.rotation);
        queue.Remove(unit);
        isTraining = false;
    }
}
