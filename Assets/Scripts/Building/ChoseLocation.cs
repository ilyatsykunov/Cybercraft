using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseLocation : MonoBehaviour {

    [SerializeField]
    protected bool isSelected;
    [SerializeField]
    private Vector3 mousePosition;
    [SerializeField]
    private float distanceToGround;

    // Use this for initialization
    void Start () {
        
        isSelected = true;
	}
	
	// Update is called once per frame
	void Update () {
        FollowMouse();

    }

    void FollowMouse()
    {
        mousePosition = Camera.main.GetComponent<MousePos>().mousePosition;

        GetDistanceToGround();
        if(distanceToGround > 1f && distanceToGround < 1.5f)
        {
            transform.position = new Vector3(mousePosition.x, transform.position.y - 0.1f, mousePosition.y);
        }
        else if (distanceToGround < 1f && distanceToGround > 0.5f)
        {
            transform.position = new Vector3(mousePosition.x, transform.position.y + 0.1f, mousePosition.y);
        }
        else
        {
            transform.position = new Vector3(mousePosition.x, transform.position.y, mousePosition.y);
        }
        

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
}
