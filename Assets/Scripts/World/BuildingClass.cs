using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClass : MonoBehaviour
{

    [SerializeField]
    protected int health;
    [SerializeField]
    public string faction;


    protected bool isBeingCaptured;
    [SerializeField]
    protected int capturePercent;



    // Use this for initialization
    void Start()
    {
        health = 100;
        capturePercent = 0;

    }

    // Update is called once per frame
    void Update()
    {
        ManageHealth();

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
    public IEnumerator Capture(string capturingFaction)
    {
        if (capturePercent >= 100)
        {
            faction = capturingFaction;
        }
        else
        {
            if (isBeingCaptured == false)
            {
                isBeingCaptured = true;
                yield return new WaitForSeconds(1);
                capturePercent += 5;
                isBeingCaptured = false;
            }
        }
    }
}
