using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseLocation : MonoBehaviour {

    [SerializeField]
    protected bool isBeingMoved;
    [SerializeField]
    protected bool canBePlaced;

    [SerializeField]
    private Vector3 mousePosition;
    [SerializeField]
    private float distanceToGround;
    private float yAxis;

    // Use this for initialization
    void Start () {

        yAxis = 2f;
        isBeingMoved = true;
        canBePlaced = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(isBeingMoved == true)
        {
            FollowMouse();
            PlaceBuilding();
        }
    }

    void PlaceBuilding()
    {
        if (Input.GetMouseButtonDown(0) && canBePlaced == true)
        {
            gameObject.transform.position = new Vector3(mousePosition.x, 1f, mousePosition.z);
            isBeingMoved = false;
        }
    }

    void FollowMouse()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
        {
            mousePosition = rayHit.point;
        }
        GetDistanceToGround();
        /*if (distanceToGround > 2f)
        {
            yAxis -= 0.01f;
        }
        else if(distanceToGround < 2f)
        {
            yAxis += 0.01f;
        }*/
        transform.position = new Vector3(mousePosition.x, yAxis, mousePosition.z);
    }
    void GetDistanceToGround()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, -Vector3.up, out rayHit, Mathf.Infinity))
        {
            if(rayHit.collider.tag == "Ground")
            {
                distanceToGround = rayHit.distance;
            }
        }
    }
    void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.tag == "Building")
        {
            canBePlaced = false;
        }
    }
    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.tag == "Building")
        {
            canBePlaced = true;
        }
    }
}
