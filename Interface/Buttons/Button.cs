using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Button : MonoBehaviour {

    public GameObject infoBox;
    public Text infoText;
    public string info;
    public GameObject assignedObject;
    public WorldController wc;
    public PlayerController pc;

    protected virtual void Start()
    {
        pc = GameObject.Find("World").GetComponent<PlayerController>();
        wc = GameObject.Find("World").GetComponent<WorldController>();
        if (!wc.buttons.Contains(gameObject))
        {
            wc.buttons.Add(gameObject);
        }
    }

    public void EnableText()
    {
        infoText.text = info;
        infoBox.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 50f, Input.mousePosition.z);
        infoBox.SetActive(true);
        
    }
    public void DisableText()
    {
        infoBox.SetActive(false);
    }
}
