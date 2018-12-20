using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float panSpeed = 20f;
    public float scrollSpeed = 1000f;
    public float panBorderThickness = 25f;
    public bool borderScroll = false;
    public Vector2 panLimit;
    private float minY = 5f;
    private float maxY = 30f;

    private float mouseX;
    private float mouseY;

    public float xRotationMin = -15f;
    public float xRotationMax = 90f;

    private float cameraRotationX;
    private float targetRotationX;

    // Update is called once per frame
    void Update()
    {
        MouseRotation();
        MouseMovement();
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
    }
    void MouseMovement()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickness && borderScroll == true) || (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.y < mouseY))
        {
            pos += transform.forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || (Input.mousePosition.y <= panBorderThickness && borderScroll == true) || (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.y > mouseY))
        {
            pos -= transform.forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickness && borderScroll == true) || (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.x < mouseX))
        {
            pos += transform.right * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || (Input.mousePosition.x <= panBorderThickness && borderScroll == true) || (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftControl) && Input.mousePosition.x > mouseX))
        {
            pos -= transform.right * panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
    void MouseRotation()
    {
        float easeFactor = 10f;
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.mousePosition.x != mouseX)
            {
                float cameraRotationY = (Input.mousePosition.x - mouseX) * easeFactor * Time.deltaTime;
                gameObject.transform.Rotate(0f, cameraRotationY, 0f);
            }
            if (Input.mousePosition.y != mouseY)
            {
                cameraRotationX = (mouseY - Input.mousePosition.y) * easeFactor * Time.deltaTime;
                targetRotationX = Camera.main.transform.eulerAngles.x + cameraRotationX;
                if(targetRotationX > 300f)
                {
                    targetRotationX -= targetRotationX;
                }
                if(targetRotationX >= xRotationMin && targetRotationX <= xRotationMax)
                {
                    Camera.main.transform.Rotate(cameraRotationX, 0f, 0f);
                }
                
            }
        }
    }
}
