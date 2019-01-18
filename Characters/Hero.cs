/// <summary>
/// Controller for playable humanoid GameObjects.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    protected bool isSelected;
    public float spottingDistance;
    public PlayerController pc;

    public GameObject kidnappingTarget;
    public GameObject kidnappedCivilian;
    public GameObject kidnappingHolder;

    public bool isKidnapping;

    // Use this for initialization
    protected override void Start()
    {
        pc = GameObject.Find("World").GetComponent<PlayerController>();
        isSelected = false;
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ChangeAnimation();
        if (gameObject.GetComponent<ClickOn>().currentlySelected == true)
        {
            GetDirection();
        }
        if(kidnappingTarget != null)
        {
            Kidnap(kidnappingTarget);
        }
    }
    protected void GetDirection()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftControl))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
            {
                if(pc.isSelectingPatrol == false)
                {
                    patrolMode = false;
                    patrolPoint1.SetActive(false);
                    patrolPoint2.SetActive(false);
                    if (rayHit.collider.gameObject.tag == "Enemy")
                    {
                        attackTarget = rayHit.collider.gameObject;
                    }
                    else if (rayHit.collider.gameObject.tag == "Building" && pc.isSelectingCapture  == true)
                    {
                        targetBuilding = rayHit.collider.gameObject;
                        pc.CancelEverything();
                    }
                    else if(rayHit.collider.gameObject.tag == "Human" && rayHit.collider.gameObject.GetComponent<TargetCivilian>() != null)
                    {
                        kidnappingTarget = rayHit.collider.gameObject;
                        target = kidnappingTarget.transform.position;
                    }
                    else
                    {
                        attackTarget = null;
                        kidnappingTarget = null;
                        target = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                    }
                }
                else if (pc.isSelectingPatrol == true)
                {
                    if(patrolPoint1.activeInHierarchy == false || patrolPoint2.activeInHierarchy == true)
                    {
                        patrolPoint1.transform.position = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                        patrolPoint1.SetActive(true);
                        patrolPoint2.SetActive(false);
                    }
                    else if(patrolPoint1.activeInHierarchy == true && patrolPoint2.activeInHierarchy == false)
                    {
                        patrolPoint2.transform.position = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                        patrolPoint2.SetActive(true);
                        pc.CancelEverything();
                    }
                }

            }
        }
    }
    public void Stop()
    {
        target = transform.position;
        kidnappingTarget = null;
        RemoveTarget();
    }
    public void Kidnap(GameObject kidnapTarget)
    {
        float distance = Vector3.Distance(transform.position, kidnapTarget.transform.position);
        if(distance <= 1f)
        {
            StartCoroutine("KidnapRoutine", kidnapTarget);
        }
        else
        {
            Move();
        }
    }
    private IEnumerator KidnapRoutine(GameObject kidnapTarget)
    {
        isKidnapping = true;
        kidnapTarget.GetComponent<TargetCivilian>().StartCoroutine("GetKidnapped", gameObject);
        kidnappedCivilian = kidnappingTarget;
        Stop();
        yield return new WaitForSeconds(2f);
        isKidnapping = false;
    }
    protected override void ChangeAnimation()
    {
        if(isKidnapping == true)
        {
            charAnimator.SetBool("isWalking", false);
            charAnimator.SetBool("isIdle", false);
            charAnimator.SetBool("isKidnapWalking", false);
            charAnimator.SetBool("isKidnapIdle", false);
            charAnimator.SetBool("isKidnapping", true);
        }
        else
        {
            charAnimator.SetBool("isKidnapping", false);
            if (kidnappedCivilian != null)
            {
                charAnimator.SetBool("isWalking", false);
                charAnimator.SetBool("isIdle", false);
                if (isMoving == true)
                {
                    charAnimator.SetBool("isKidnapWalking", true);
                    charAnimator.SetBool("isKidnapIdle", false);
                }
                else if (isMoving == false)
                {
                    charAnimator.SetBool("isKidnapWalking", false);
                    charAnimator.SetBool("isKidnapIdle", true);
                }
            }
            else if (kidnappedCivilian == null)
            {
                charAnimator.SetBool("isKidnapWalking", false);
                charAnimator.SetBool("isKidnapIdle", false);
                base.ChangeAnimation();
            }
        }
    }

}
