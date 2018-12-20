using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject launchedBy;
    private int speed;
    private float timeLeft;

    // Use this for initialization
    void Start()
    {
        speed = 10;
        timeLeft = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            Destroy(gameObject);
        }
    }
    public void ShootTo(Vector3 shootTo)
    {
        transform.LookAt(shootTo);
        //gameObject.transform.Rotate(90f, 0f, 0f, Space.World);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground" || collision.gameObject.layer == LayerMask.NameToLayer("Obstacles") || collision.gameObject.layer == LayerMask.NameToLayer("Road"))
        {
            Destroy(gameObject);
        }
    }
}
