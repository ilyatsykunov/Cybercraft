using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField]
    protected float speed;

    protected Vector2 direction;
    protected Rigidbody RB;

    [SerializeField]
    protected Vector3 target;

    protected virtual void Update()
    {
        GetDirection();
        Move();
    }

    protected void Move()
    {
        transform.Translate(target * speed * Time.deltaTime);
    }

    protected void GetDirection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity))
            {
                target = rayHit.point;
            }
        }
    }
}
