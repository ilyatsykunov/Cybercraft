using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour {

    public string currentLight;
    private Light lights;

    public Material originalGreen;
    public Material originalYellow;
    public Material originalRed;
    public Material newGreen;
    public Material newYellow;
    public Material newRed;

    private MeshRenderer meshRen;
    private Material[] materials;

    // Use this for initialization
    void Start () {
        meshRen = gameObject.GetComponent<MeshRenderer>();
        lights = gameObject.transform.Find("Light").GetComponent<Light>();
        if(currentLight == "Green")
        {
            materials = meshRen.materials;
            materials[1] = newGreen;
            materials[2] = originalYellow;
            materials[3] = originalRed;
            lights.color = Color.green;
            meshRen.materials = materials;
            StartCoroutine("ToYellow", "ToRed");
        }
        else if (currentLight == "Red")
        {
            materials = meshRen.materials;
            materials[1] = originalGreen;
            materials[2] = originalYellow;
            materials[3] = newRed;
            lights.color = Color.red;
            meshRen.materials = materials;
            StartCoroutine("ToYellow", "ToGreen");
        }

    }

    public IEnumerator ToRed()
    {
        materials = meshRen.materials;
        yield return new WaitForSeconds(3);
        currentLight = "Red";
        materials[1] = originalGreen;
        materials[2] = originalYellow;
        materials[3] = newRed;
        lights.color = Color.red;
        meshRen.materials = materials;
        StartCoroutine("ToYellow", "ToGreen");
    }
    public IEnumerator ToYellow(string next)
    {
        materials = meshRen.materials;
        yield return new WaitForSeconds(10);
        currentLight = "Yellow";
        materials[1] = originalGreen;
        materials[2] = newYellow;
        materials[3] = originalRed;
        lights.color = Color.yellow;
        meshRen.materials = materials;
        StartCoroutine(next);
    }
    public IEnumerator ToGreen()
    {
        materials = meshRen.materials;
        yield return new WaitForSeconds(3);
        currentLight = "Green";
        materials[1] = newGreen;
        materials[2] = originalYellow;
        materials[3] = originalRed;
        lights.color = Color.green;
        meshRen.materials = materials;
        StartCoroutine("ToYellow", "ToRed");
    }
}
