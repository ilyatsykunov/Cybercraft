using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestroyButton : Button {

    protected override void Start()
    {
        base.Start();
    }

    public void DestroyBuilding()
    {
        if(assignedObject != null && assignedObject.tag == "Building" && assignedObject.GetComponent<Building>().faction == "First")
        {
            assignedObject.SetActive(false);
            GameObject world = GameObject.Find("World");
            world.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }
}
