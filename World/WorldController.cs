using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public PlayerController pc;

    public Text money;
    public Text textGO;
    public Text textGO2;
    public Canvas canvas;
    public Text priceGO;
    public GameObject percentageBar;

    public GameObject emptyScreen;
    public GameObject buildScreen;
    public GameObject unitScreen;
    public GameObject unitSpawnScreen1;
    public GameObject buildingSpawnScreen;
    public List<GameObject> buttons = new List<GameObject>();

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }
    private void Update()
    {
        money.text = "£" + pc.moneyAmount.ToString();
    }

    public void SpawnBuilding(GameObject building)
    {
        
        if(GetComponent<PlayerController>().moneyAmount >= building.GetComponent<Building>().price)
        {
            Instantiate(building);
            priceGO.color = Color.white;
        }
        else
        {
            priceGO.color = Color.red;
        }
        
    }
    public void ActivateScreen(GameObject screenToActivate)
    {
        if(screenToActivate.activeInHierarchy == false)
        {
            emptyScreen.SetActive(false);
            buildScreen.SetActive(false);
            unitScreen.SetActive(false);
            unitSpawnScreen1.SetActive(false);
            buildingSpawnScreen.SetActive(false);
            screenToActivate.SetActive(true);
        }

    }
    public void ChangeText(string textToDisplay, string textToDisplay2)
    {
        textGO.text = textToDisplay;
        textGO2.text = textToDisplay2;
    }
    public void ChangePercentageBar(GameObject building)
    {
        percentageBar.GetComponent<PercentageBar>().attachedBuilding = building;
    }
    public void AssignButtons(GameObject assignedObject)
    {
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().assignedObject = assignedObject;
        }
    }
}
