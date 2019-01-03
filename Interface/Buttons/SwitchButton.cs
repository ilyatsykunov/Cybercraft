using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : Button
{
    protected override void Start()
    {
        base.Start();
    }

    public void SwitchWeapons()
    {
        pc.isSelectingPatrol = false;
        pc.isSelectingCapture = false;
        if (assignedObject != null && assignedObject.tag == "Player")
        {
            if (assignedObject.GetComponent<Character>().meleeAttack == true)
            {
                assignedObject.GetComponent<Character>().meleeAttack = false;
            }
            else
            {
                assignedObject.GetComponent<Character>().meleeAttack = true;
            }
        }
    }
}