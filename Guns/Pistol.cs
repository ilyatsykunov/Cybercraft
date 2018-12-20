using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun {

	// Use this for initialization
	protected override void Awake() {
	//Ammo
    magazineTotal = 9;
    magazineCurrent = 9;
    totalCapacity = 81;
    currentTotal = 81;
    //Stats
    firingRate = 1f;
    maxDamage = 12f;
    reloadTime = 1.5f;
}

    // Update is called once per frame
    protected override void Update() {
        base.Update();
	}
}
