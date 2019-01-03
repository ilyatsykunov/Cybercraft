using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    public Canvas canvas;
    public GameObject miniMapHolder;
    public Image square;
    public float width;
    public float height;

    public List<mapPoint> listOfPoints = new List<mapPoint>();

	// Use this for initialization
	void Start ()
    {
        width = gameObject.GetComponent<RectTransform>().rect.width;
        height = gameObject.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update ()
    {
        DrawMap();
        UpdateMap();
    }
    void UpdateMap()
    {
        foreach(mapPoint point in listOfPoints)
        {
            if (point.objectIn3DSpace.activeInHierarchy == true)
            {
                if(point.objectIn2DSpace.activeInHierarchy == false)
                {
                    point.objectIn2DSpace.SetActive(true);
                }
                point.UpdateLocation();
            }
            else
            {
                point.objectIn2DSpace.SetActive(false);
            }
        }
    }
    void DrawMap()
    {
        GameObject[] friendlyUnits = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject unit in friendlyUnits)
        {
            Vector2 mapPos = new Vector2(unit.transform.position.x, unit.transform.position.z);
            if (unit.GetComponent<Character>().miniMapPoint == null)
            {
                Image point = Instantiate(square, mapPos, Quaternion.identity, miniMapHolder.transform) as Image;
                unit.GetComponent<Character>().miniMapPoint = point.gameObject;
                point.color = Color.green;
                mapPoint newPoint = new mapPoint(unit, point.gameObject, mapPos);
                listOfPoints.Add(newPoint);
            }
        }
    }
}
