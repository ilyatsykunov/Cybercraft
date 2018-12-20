using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapPoint{

    public GameObject objectIn3DSpace;
    public GameObject objectIn2DSpace;
    public Vector2 location;

    public void UpdateLocation()
    {
        location = new Vector2(objectIn3DSpace.transform.position.x, objectIn3DSpace.transform.position.z);
        objectIn2DSpace.transform.localPosition = location/2;
    }

    public mapPoint(GameObject newObjectIn3DSpace, GameObject newObjectIn2DSpace, Vector2 newLocation)
    {
        objectIn3DSpace = newObjectIn3DSpace;
        objectIn2DSpace = newObjectIn2DSpace;
        location = newLocation;
    }

}
