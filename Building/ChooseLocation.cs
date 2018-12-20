using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChooseLocation : MonoBehaviour
{

    [SerializeField]
    protected bool isBeingMoved;
    [SerializeField]
    protected bool canBePlaced;
    [SerializeField]
    private Vector3 mousePosition;
    [SerializeField]
    private float distanceToGround;
    private float yAxis;

    private MeshRenderer rend;
    public Material greenMaterial;
    public Material redMaterial;
    public Material[] originalMaterials;

    // Use this for initialization
    void Start()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        yAxis = 2f;
        isBeingMoved = true;
        canBePlaced = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingMoved == true)
        {
            FollowMouse();
            PlaceBuilding();
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
            if(canBePlaced == false)
            {
                Material[] materials = rend.materials;
                for(int i = 0; i < materials.Length; i++)
                {
                    materials[i] = redMaterial;
                }
                rend.materials = materials;
            }
            else
            {
                Material[] materials = rend.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = greenMaterial;
                }
                rend.materials = materials;
            }
        }
        else
        {
            if(rend.materials != originalMaterials)
            {
                rend.materials = originalMaterials;
            }
        }
        
    }

    void PlaceBuilding()
    {
        if (Input.GetKey(KeyCode.R))
        {
            float rotateBy =  100f * Time.deltaTime;
            gameObject.transform.Rotate(0f, rotateBy, 0f);
        }

        if (Input.GetMouseButtonDown(0) && canBePlaced == true)
        {
            
            gameObject.transform.position = new Vector3(mousePosition.x, 1f, mousePosition.z);
            isBeingMoved = false;
            GameObject world = GameObject.Find("World");
            world.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }

    void FollowMouse()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
        {
            mousePosition = rayHit.point;
        }
        gameObject.GetComponent<ClickOn>().currentlySelected = true;
        gameObject.GetComponent<ClickOn>().ClickMe();
        //Make it float above the ground depending on surface height
        GetDistanceToGround();
        /*if (distanceToGround > 2f)
        {
            yAxis -= 0.01f;
        }
        else if(distanceToGround < 2f)
        {
            yAxis += 0.01f;
        }*/
        transform.position = new Vector3(mousePosition.x, yAxis, mousePosition.z);
    }
    void GetDistanceToGround()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, -Vector3.up, out rayHit, Mathf.Infinity))
        {
            if (rayHit.collider.tag == "Ground")
            {
                distanceToGround = rayHit.distance;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacles") || other.gameObject.layer == LayerMask.NameToLayer("Road"))
        {
            canBePlaced = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles") || other.gameObject.layer == LayerMask.NameToLayer("Road"))
        {
            canBePlaced = true;
        }
    }

}
