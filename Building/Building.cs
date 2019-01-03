using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Building : MonoBehaviour
{
    public int price;
    public float income = 2f;
    public int health;
    [SerializeField]
    public string faction;
    public GameObject doors;

    [SerializeField]
    public int percentage;
    public bool beingDefended;

    //public WorldController WC;
    public GameObject screen;
    public GameObject canvas;
    public string textToDisplay;

    //Entering
    public List<GameObject> unitsInside = new List<GameObject>();
    private List<GameObject> unitsGone = new List<GameObject>();

    // Use this for initialization
    protected virtual void Start()
    {
        if(screen == null)
        {
            screen = canvas.transform.Find("BuildingScreen").gameObject;
        }
        doors = gameObject.transform.Find("Doors").gameObject;
        health = 100;
        percentage = 0;
        //WC = GameObject.Find("World").GetComponent<WorldController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(doors == null)
        {
            doors = gameObject.transform.Find("Doors").gameObject;
        }
        ManageHealth();
        if(unitsInside.Count > 0)
        {
            ManageUnitsInside();
        }
        else
        {
            StopCoroutine("Capture");
        }
    }

    protected void ManageHealth()
    {
        if (health > 100)
        {
            health = 100;
        }
        if (health <= 0)
        {
            health = 0;
        }
    }
    public void ManageUnitsInside()
    {
        foreach(GameObject unit in unitsInside)
        {
            if(unit.tag == "Human")
            {
                StartCoroutine("Wait", unit);
            }
            AmmoRefill();
        }
        foreach(GameObject unit in unitsGone)
        {
            if (unitsInside.Contains(unit))
            {
                unitsInside.Remove(unit);
            }
        }
        unitsGone.RemoveRange(0, unitsGone.Count);
    }
    public void DestroyBuilding()
    {
        if(health <= 0 || faction == "First")
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator Wait(GameObject unit)
    {
        float seconds = Random.Range(5f, 35f);
        yield return new WaitForSeconds(seconds);
        ResetUnit(unit);
    }
    public IEnumerator Capture(string capturingFaction)
    {
        if (percentage >= 100)
        {
            faction = capturingFaction;
            percentage = 0;
            foreach(GameObject unit in unitsInside)
            {
                if(unit.tag != "Human")
                {
                    ResetUnit(unit);
                }
            }
            yield break;
        }
        else
        {
            foreach (GameObject unit in unitsInside)
            {
                if (unit.GetComponent<Character>() != null && unit.GetComponent<Character>().faction != capturingFaction || unit.tag == "Enemy")
                {
                    ResetUnit(unit);
                    percentage = 0;
                    StopCoroutine("Capture");
                }
            }
            yield return new WaitForSeconds(price/100);
            percentage += 5;
            StartCoroutine("Capture", capturingFaction);
        }
    }
    private void AmmoRefill()
    {
        foreach(GameObject unit in unitsInside)
        {
            if((unit.tag == "Player" || unit.tag == "Enemy") && unit.GetComponent<Character>().weapon != null)
            {
                unit.GetComponent<Character>().weapon.GetComponent<Gun>().magazineCurrent = unit.GetComponent<Character>().weapon.GetComponent<Gun>().magazineTotal;
                unit.GetComponent<Character>().weapon.GetComponent<Gun>().currentTotal = unit.GetComponent<Character>().weapon.GetComponent<Gun>().totalCapacity;
            }
        }
    }
    private void ResetUnit(GameObject unit)
    {
        unit.SetActive(true);
        unit.GetComponent<Human>().isInsideBuilding = false;
        unit.GetComponent<Human>().targetBuilding = null;
        unitsGone.Add(unit);
    }
}
