using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 minSize, maxSize;
    enum State {turn, move, wait};
    State state = State.turn;

    Vector3 velocity;
    Vector3 direction = new Vector3(1,0,0);
    float speed = 0;

    Vector3 target;

    float heading = 0;

    float t = 0;

    GameObject tankSheet;
    Animator animator;

    void Start()
    {
        minSize = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maxSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        tankSheet = GameObject.Find("tanksheet_9");
        animator = tankSheet.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed * Time.deltaTime;
        transform.position += velocity;

        if (state == State.turn)
        {
            animator.speed = 0;
            target = selectRandomPosition();
            Vector3 differenceVector = target - transform.position;
            heading = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, heading);
            state = State.move;
        }

        if (state ==State.move)
        {
            animator.speed = 1;
            direction = new Vector3(Mathf.Cos(heading * Mathf.Deg2Rad), Mathf.Sin(heading * Mathf.Deg2Rad),0);
            speed = 2f;
            float distance = (target - transform.position).magnitude;
            if(distance < 0.2f)
            {
                speed = 0;
                animator.speed = 0;
                state = State.wait;
            }
        }

        if (state == State.wait)
        {
            state = State.turn;

        }
    }

    Vector3 selectRandomPosition()
    {
        float randomX = Random.Range(minSize.x, maxSize.x);
        float randomY = Random.Range(minSize.y, maxSize.y);
        return new Vector3 (randomX, randomY, 0);
    }
}
