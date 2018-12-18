using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject launchedBy;
    private int speed;

    // Use this for initialization
    void Start()
    {
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }
    public void ShootTo(Vector3 shootTo)
    {
        transform.LookAt(shootTo);
        //gameObject.transform.Rotate(45f, -90f, 45f, Space.World);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Building" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
