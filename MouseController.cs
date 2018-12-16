using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    public Camera cam;
    public Vector3 mousePos;
    public GameObject chosenTile;
    public Material selectedMaterial;

    //Multiple selection
    //public List<GameObject> selectedTiles = new List<GameObject>();
    //public GameObject selectionBox;

    void Start()
    {

    }


    void Update()
    {
        MousePosition();
    }

    void MousePosition()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            chosenTile = hit.collider.gameObject;
            chosenTile.GetComponent<MeshRenderer>().material = selectedMaterial;
            if (!hit.collider.gameObject)
            {
                chosenTile = null;
            }
        }
    }

    /*void MultipleSelection(){
		if (Input.GetMouseButtonDown(0)) {
			selectedTiles.Clear ();
			Vector3 startT = Input.mousePosition;
			if (Input.GetMouseButtonUp (0)) {
				Vector3 endT = Input.mousePosition;
				Vector3 middlePoint = Vector3.Lerp (startT, endT, 0.5f);
				//selectionBox.transform.position = middlePoint;
				float SBwidth = Mathf.Max (startT.x, endT.x) - Mathf.Min (startT.x, endT.x);
				float SBheight = Mathf.Max (startT.z, endT.z) - Mathf.Min (startT.z, endT.z);
				Rect box = new Rect();
				box.center = middlePoint;
				box.width = SBwidth;
				box.height = SBheight;
				Instantiate (box);

			}
		}

	}*/


}
