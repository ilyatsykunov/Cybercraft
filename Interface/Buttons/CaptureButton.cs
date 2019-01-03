using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureButton : Button {

    protected override void Start()
    {
        base.Start();
    }
    public void SelectBuilding()
    {
        pc.isSelectingPatrol = false;
        pc.isSelectingCapture = true;
    }
}
