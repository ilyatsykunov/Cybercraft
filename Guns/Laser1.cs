using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser1 : Gun {

    // Use this for initialization
    protected override void Awake()
    {
        //Ammo
        magazineTotal = 5;
        magazineCurrent = 5;
        totalCapacity = 40;
        currentTotal = 40;
        //Stats
        firingRate = 1.5f;
        maxDamage = 25f;
        reloadTime = 3f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
