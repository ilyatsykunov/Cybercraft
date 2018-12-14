using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private Material origMaterial;
    public GameObject MS_holder;
    public MouseController MS;

    // Use this for initialization
    void Start()
    {
        origMaterial = gameObject.GetComponent<MeshRenderer>().material;
        MS = MS_holder.GetComponent<MouseController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (MS.chosenTile != this.gameObject && gameObject.GetComponent<MeshRenderer>().material != origMaterial)
        {
            gameObject.GetComponent<MeshRenderer>().material = origMaterial;
        }
    }
}
