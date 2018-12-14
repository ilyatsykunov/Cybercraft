using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{

    [SerializeField]
    private Material notSelected;
    [SerializeField]
    private Material selected;

    private MeshRenderer myRend;

    [HideInInspector]
    public bool currentlySelected = false;

    void Start()
    {

        myRend = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);
        ClickMe();
    }


    public void ClickMe()
    {
        if (currentlySelected == false)
        {
            myRend.material = notSelected;
        }
        else
        {
            myRend.material = selected;
        }
    }
}

