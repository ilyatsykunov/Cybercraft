using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building {

    public GameObject unit1;

    private bool isTraining;

    public List<GameObject> queue = new List<GameObject>();

	protected override void Start () {
        base.Start();
        isTraining = false;
	}

    protected override  void Update () {
        base.Update();
        if(queue.Count > 0)
        {
            SpawnUnits();
        }
	}
    public void AddToQueue(GameObject unitToSpawn)
    {
        if(queue.Count < 5)
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
                StartCoroutine("Training");
                Instantiate(unit, doors.transform.position, unit.transform.rotation);
                queue.Remove(unit);
            }
        }
    }
    protected IEnumerator Training()
    {
        isTraining = true;
        yield return new WaitForSeconds(5f);
        isTraining = false;
    }
}
