using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral : Building {

    public string screenText;

    // Use this for initialization
    protected override void Start () {
        screenText = "This is a neutral building";
        faction = "Neutral";
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}
}
