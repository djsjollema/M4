using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    Vector3 velocity;
    Vector3 direction = new Vector3(1,0,0);
    float speed = 0f;
    Animator animator;
    float heading = 0f;
    Vector2 minSize, maxSize;
    GameObject TankSheet;
    [SerializeField] TankBullet tankBullet;
    
    void Start()
    {
        TankSheet = GameObject.Find("tanksheet_1");
        //tankBullet = GameObject.Find("TankBullet");

        animator = TankSheet.GetComponent<Animator>();
        animator.speed = 0;
        minSize = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maxSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        speed = Input.GetAxisRaw("Vertical") * 2;
        direction = new Vector3(Mathf.Cos(heading * Mathf.Deg2Rad), Mathf.Sin(heading * Mathf.Deg2Rad), 0);
        velocity = direction * speed * Time.deltaTime;
        transform.position += velocity;

        heading -= Input.GetAxisRaw("Horizontal");
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, heading));

        if(speed >= 0)
        {
            animator.Play("Forward");
        } else
        {
            animator.Play("Back");
        }
        animator.speed = Mathf.Abs(speed);
        keepInBox();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TankBullet shot = Instantiate(tankBullet, transform.position, Quaternion.identity);
            shot.heading = this.heading;

        }
    }


    void keepInBox()
    {
        if (transform.position.x < minSize.x)
        {
            transform.position = new Vector3(maxSize.x, transform.position.y, 0);
        }

        if (transform.position.x > maxSize.x)
        {
            transform.position = new Vector3(minSize.x, transform.position.y, 0);
        }

        if (transform.position.y < minSize.y)
        {
            transform.position = new Vector3(transform.position.x, maxSize.y, 0);
        }

        if (transform.position.y > maxSize.y)
        {
            transform.position = new Vector3(transform.position.x, minSize.y, 0);
        }
    }
}
