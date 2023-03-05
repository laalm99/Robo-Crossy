using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBehaviour : MonoBehaviour
{

    private float speed;

    private void Start()
    {
        speed = 5;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }
    }
}
