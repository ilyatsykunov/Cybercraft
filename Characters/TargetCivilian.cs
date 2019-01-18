using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetCivilian : Civilian {

    public bool hasBeenSpotted;
    public bool isKidnapped;
    public GameObject heldBy;
    public Vector3 area;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        isKidnapped = false;
    }
	
	// Update is called once per frame
	protected override void Update () {
        ManageHealth();
        StartCoroutine("GetSpeed");
        ChangeAnimation();
        float distance = Vector3.Distance(target, area);
        if(heldBy == null)
        {
            if (transform.position != target && isMoving == false && isInsideBuilding == false && distance < 50f)
            {
                Move();
            }
            if (isMoving == false && isInsideBuilding == false)
            {
                if (targetBuilding != null)
                {
                    float doorDistance = Vector3.Distance(transform.position, targetBuilding.GetComponent<Building>().doors.transform.position);
                    if (doorDistance <= 1f)
                    {
                        EnterBuilding();
                    }
                }
                else
                {
                    RandomTarget();
                }
            }
        }
        else
        {
            if (heldBy.GetComponent<Human>().isMoving == true)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

    }
    public void getKidnapped(GameObject kidnappedBy)
    {
        heldBy = kidnappedBy;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        transform.rotation = kidnappedBy.transform.rotation;
        transform.position = kidnappedBy.GetComponent<Hero>().kidnappingHolder.transform.position;
        target = kidnappedBy.GetComponent<Hero>().kidnappingHolder.transform.position;
        gameObject.transform.parent = kidnappedBy.transform;
    }
    public IEnumerator GetKidnapped(GameObject kidnappedBy)
    {
        charAnimator.SetBool("isBeingKidnapped", true);
        heldBy = kidnappedBy;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        transform.rotation = kidnappedBy.transform.rotation;
        transform.position = kidnappedBy.GetComponent<Hero>().kidnappingHolder.transform.position;
        target = kidnappedBy.GetComponent<Hero>().kidnappingHolder.transform.position;
        gameObject.transform.parent = kidnappedBy.transform;
        yield return new WaitForSeconds(1f);
        charAnimator.SetBool("isBeingKidnapped", false);
        isKidnapped = true;
    }
    protected override void ChangeAnimation()
    {
        if (isKidnapped == true)
        {
            charAnimator.SetBool("isBeingKidnapped", false);
            charAnimator.SetBool("isWalking", false);
            charAnimator.SetBool("isIdle", false);
            if(isMoving == true)
            {
                charAnimator.SetBool("isKidnappedWalking", true);
                charAnimator.SetBool("isKidnappedIdle", false);
            }
            else if(isMoving == false)
            {
                charAnimator.SetBool("isKidnappedWalking", false);
                charAnimator.SetBool("isKidnappedIdle", true);
            }
        }
        else if (isKidnapped == false)
        {
            charAnimator.SetBool("isKidnappedWalking", false);
            charAnimator.SetBool("isKidnappedIdle", false);
            base.ChangeAnimation();
        }

    }
}
