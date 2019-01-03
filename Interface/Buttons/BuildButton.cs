using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : Button {

    protected override void Start()
    {
        base.Start();
        info = "Build " + assignedObject.name;
    }

}
