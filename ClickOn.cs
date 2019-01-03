using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{

    public GameObject selectionLight;
    public GameObject targetHolder;
    public Vector3 oldTarget;

    public bool currentlySelected = false;

    void Start()
    {
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);
        ClickMe();
    }
    private void Update()
    {
        if(gameObject.tag == "Player")
        {
            HeroTarget();
        }
    }


    public void ClickMe()
    {
        if (currentlySelected == false && selectionLight != null)
        {
            selectionLight.SetActive(false);
        }
        else if (currentlySelected == true && selectionLight != null)
        {
            selectionLight.SetActive(true);
        }
    }

    private void HeroTarget()
    {
        if (currentlySelected == true && gameObject.GetComponent<Human>().target != gameObject.transform.position && gameObject.GetComponent<Hero>() != null)
        {
            if (gameObject.GetComponent<Human>().target != oldTarget)
            {
                StopAllCoroutines();
                targetHolder.SetActive(true);
                Vector3 newPos = new Vector3(gameObject.GetComponent<Human>().target.x, targetHolder.transform.position.y, gameObject.GetComponent<Human>().target.z);
                targetHolder.transform.position = newPos;
                oldTarget = gameObject.GetComponent<Human>().target;
                StartCoroutine("Wait");
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        targetHolder.SetActive(false);
    }
}

