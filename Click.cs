using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    [SerializeField]
    private LayerMask clickablesLayer;
    [SerializeField]
    private LayerMask obstaclesLayer;

    private List<GameObject> selectedObjects;


    [HideInInspector]
    public List<GameObject> selectableObjects;

    private Vector3 mousePos1;
    private Vector3 mousePos2;

    public WorldController WC;

    private void Awake()
    {
        selectableObjects = new List<GameObject>();
        selectedObjects = new List<GameObject>();
    }

    void Start()
    {
        WC = GameObject.Find("World").GetComponent<WorldController>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer))
            {
                WC.ActivateScreen(rayHit.collider.GetComponent<Human>().screen);
                WC.ChangeText(rayHit.collider.GetComponent<Human>().textToDisplay);
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (clickOnScript.currentlySelected == false)
                    {
                        selectedObjects.Add(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = true;
                        clickOnScript.ClickMe();
                    }
                    else
                    {
                        selectedObjects.Remove(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = false;
                        clickOnScript.ClickMe();
                    }

                }
                else
                {
                    ClearSelection();

                    selectedObjects.Add(rayHit.collider.gameObject);
                    clickOnScript.currentlySelected = true;
                    clickOnScript.ClickMe();
                }


            }
            else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, obstaclesLayer))
            {
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();
                if(rayHit.collider.tag == "Building")
                {
                    WC.ActivateScreen(rayHit.collider.GetComponent<Building>().screen);
                    WC.ChangeText(rayHit.collider.GetComponent<Building>().textToDisplay);
                    ClickOn clickOnScriptBuilding = rayHit.collider.GetComponent<ClickOn>();
                    selectedObjects.Add(rayHit.collider.gameObject);
                    clickOnScriptBuilding.currentlySelected = true;
                    clickOnScriptBuilding.ClickMe();
                }
            }
            else
            {
                ClearSelection();
            }

            }

            if (Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearSelection();
        }
    }

    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            ClearSelection();
        }

        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach (GameObject selectObject in selectableObjects)
        {
            if (selectObject != null)
            {
                if (selectRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedObjects.Add(selectObject);
                    selectObject.GetComponent<ClickOn>().currentlySelected = true;
                    selectObject.GetComponent<ClickOn>().ClickMe();
                }
            }
            else
            {
                remObjects.Add(selectObject);
            }
        }

        if (remObjects.Count > 0)
        {
            foreach (GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }
            remObjects.Clear();
        }
    }

    void ClearSelection()
    {
        WC.ActivateScreen(WC.buildScreen);
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                obj.GetComponent<ClickOn>().currentlySelected = false;
                obj.GetComponent<ClickOn>().ClickMe();
            }
            selectedObjects.Clear();
        }
    }
}
