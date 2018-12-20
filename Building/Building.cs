using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Building : MonoBehaviour
{

    [SerializeField]
    protected int health;
    [SerializeField]
    public string faction;
    public GameObject doors;

    protected bool isBeingCaptured;
    [SerializeField]
    protected int capturePercent;

    //public WorldController WC;
    public GameObject screen;
    public GameObject canvas;
    public string textToDisplay;

    // Use this for initialization
    protected virtual void Start()
    {
        screen = canvas.transform.Find("UnitSpawnScreen").gameObject;
        health = 100;
        capturePercent = 0;
        //WC = GameObject.Find("World").GetComponent<WorldController>();
    }

    // Update is called once per frame
    protected virtual void Update()
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
            capturePercent = 0;
            yield break;
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
