using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour {

    public string currentLight;

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
        currentLight = "Green";
        StartCoroutine("ToYellow", "ToRed");
    }

    public IEnumerator ToRed()
    {
        materials = meshRen.materials;
        yield return new WaitForSeconds(3);
        currentLight = "Red";
        materials[2] = originalYellow;
        materials[3] = newRed;
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
        meshRen.materials = materials;
        StartCoroutine(next);
    }
    public IEnumerator ToGreen()
    {
        materials = meshRen.materials;
        yield return new WaitForSeconds(2);
        currentLight = "Green";
        materials[1] = newGreen;
        materials[2] = originalYellow;
        meshRen.materials = materials;
        StartCoroutine("ToYellow", "ToRed");
    }
}
