using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject spawnPoint;
    public GameObject unitPrefab1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach(GameObject building in buildings)
        {
            if(building.GetComponent<Building>().percentage > 0 && building.GetComponent<Building>().beingDefended == false)
            {
                DefendBuilding(building);
            }
        }
	}
    void DefendBuilding(GameObject building)
    {
        int numberOfUnits = building.GetComponent<Building>().price / 100;
        for(int i = 0; i < numberOfUnits; i++)
        {
            var unit = Instantiate(unitPrefab1, spawnPoint.transform.position, Quaternion.identity) as GameObject;
            unit.GetComponent<Human>().targetBuilding = building;
        }
        building.GetComponent<Building>().beingDefended = true;
    }
}
