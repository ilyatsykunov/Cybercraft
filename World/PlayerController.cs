using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int moneyAmount;
    public List<GameObject> ownedBuildings = new List<GameObject>();
    public float moneyIncome;

    public bool isSelectingPatrol;
    public bool isSelectingCapture;

    // Use this for initialization
    void Start () {
        StartCoroutine("passiveIncome", 2f);
        if(isSelectingPatrol == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isSelectingPatrol = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        FindBuildings();
        moneyIncome = MoneyIncome(ownedBuildings);
	}
    private void FindBuildings()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach(GameObject building in buildings)
        {
            if(building.GetComponent<Building>().faction == "First")
            {
                if (!ownedBuildings.Contains(building))
                {
                    ownedBuildings.Add(building);
                }
            }
            else
            {
                if (ownedBuildings.Contains(building))
                {
                    ownedBuildings.Remove(building);
                }
            }
        }
        foreach(GameObject building in ownedBuildings)
        {
            int index = Array.IndexOf(buildings, building);
            if (index < 0)
            {
                ownedBuildings.Remove(building);
            }
        }
        
    }
    private IEnumerator passiveIncome(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        moneyAmount += Mathf.RoundToInt(moneyIncome);
        StartCoroutine("passiveIncome", 2f);
    }
    static float MoneyIncome(List<GameObject> buildings)
    {
        float income = 0;
        foreach (GameObject building in buildings)
        {
            income += building.GetComponent<Building>().income;
        }
        return income;
    }
}
