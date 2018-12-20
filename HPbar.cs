using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    private Human parentScript;
    private float originalScale;

    void Awake()
    {
        originalScale = gameObject.transform.localScale.x;
        parentScript = GetComponentInParent<Human>();
    }
    void Update()
    {
        ChangeSizeOfHealthBar();
        transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.parent.gameObject.transform.eulerAngles.y, transform.eulerAngles.z);
    }
    void ChangeSizeOfHealthBar()
    {
        float health = parentScript.health * originalScale / 100;
        float scrollIncrease = Camera.main.transform.parent.gameObject.transform.position.y * originalScale / 10;
        gameObject.transform.localScale = new Vector3(health * scrollIncrease * 5, scrollIncrease, gameObject.transform.localScale.z);
    }
}

