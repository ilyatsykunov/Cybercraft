using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public Text textGO;
    public Canvas canvas;
    public GameObject emptyScreen;
    public GameObject buildScreen;
    public GameObject unitScreen;
    public GameObject unitSpawnScreen1;
    public GameObject buildingSpawnScreen;

    public void SpawnBuilding(GameObject building)
    {
        Instantiate(building);
    }
    public void ActivateScreen(GameObject screenToActivate)
    {
        emptyScreen.SetActive(false);
        buildScreen.SetActive(false);
        unitScreen.SetActive(false);
        unitSpawnScreen1.SetActive(false);
        buildingSpawnScreen.SetActive(false);
        screenToActivate.SetActive(true);
    }
    public void ChangeText(string textToDisplay)
    {
        textGO.text = textToDisplay;
    }
}
