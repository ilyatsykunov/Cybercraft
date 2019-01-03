using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIButton : MonoBehaviour {

    public GameObject assignedObject;
    public WorldController wc;

    protected virtual void Start()
    {
        wc = GameObject.Find("World").GetComponent<WorldController>();
        if (!wc.buttons.Contains(gameObject))
        {
            wc.buttons.Add(gameObject);
        }
    }
}
