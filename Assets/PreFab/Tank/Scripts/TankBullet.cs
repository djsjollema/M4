using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    public float heading = 0f;

    Vector3 direction = new Vector3(1,0,0);
    float speed = 5;
    Vector3 velocity;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        direction = new Vector3(Mathf.Cos(heading * Mathf.Deg2Rad), Mathf.Sin(heading * Mathf.Deg2Rad), 0);
        velocity = direction * speed * Time.deltaTime;
        transform.position += velocity;

        
    }
}
