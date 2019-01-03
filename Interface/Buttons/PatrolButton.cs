using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolButton : Button {

    protected override void Start()
    {
        base.Start();
    }
    public void StartSelecting()
    {
        pc.isSelectingPatrol = true;
        pc.isSelectingCapture = false;
    }
}
