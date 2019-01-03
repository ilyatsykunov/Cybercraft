using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : Button {

    private Click click;

    protected override void Start()
    {
        base.Start();
        click = Camera.main.GetComponent<Click>();
    }

    public void Stop()
    {
        pc.isSelectingPatrol = false;
        pc.isSelectingCapture = false;
        List<GameObject> selectedObj = click.selectedObjects;
        if(selectedObj[0].tag == "Building")
        {
            foreach (GameObject unit in selectedObj[0].GetComponent<BaseBuilding>().queue)
            {
                selectedObj[0].GetComponent<BaseBuilding>().CancelSpawn(unit);
            }
        }
        else if (selectedObj[0].tag == "Player")
        {
            foreach(GameObject unit in selectedObj)
            {
                unit.GetComponent<Hero>().Stop();
            }
        }
    }
}
