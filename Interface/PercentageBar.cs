using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour {

    private RectTransform bar;
    private float originalWidth;
    public GameObject attachedBuilding;
    public float percentage;

    // Use this for initialization
    void Start () {
        bar = GetComponent<RectTransform>();
	}
    private void Update()
    {
        if(attachedBuilding != null)
        {
            percentage = attachedBuilding.GetComponent<Building>().percentage;
            UpdatePercentageBar();
        }
    }

    void UpdatePercentageBar()
    {
        if(percentage != 0)
        {
            Vector3 lerpTo = new Vector3(percentage / 90, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, lerpTo, 1f * Time.deltaTime);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        
    }
}
