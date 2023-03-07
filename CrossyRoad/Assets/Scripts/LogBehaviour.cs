using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBehaviour : MonoBehaviour
{

    private float speed;
    public float Speed => speed;
    private int toggle;
    private float direction;
    public float Direction
    {
        get => direction;
        set
        {
            direction = value;
        }
    }

    private void Start()
    {
        speed = 5;
    }

    private void Update()
    {

        transform.position += new Vector3(Direction, 0f, 0f) * speed * Time.deltaTime;
        if (direction == 1 && transform.position.x < -50)
        {
            Destroy(gameObject);
        }else if(direction == -1 && transform.position.x > 50)
        {
            Destroy(gameObject);
        }
    }
}
