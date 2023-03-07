using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lamya.CrossyRoad
{
    public class CarBehaviour : MonoBehaviour
    {
        private float speed;


        private void Start()
        {

            speed = 5;
        }

        private void Update()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x < -50)
            {
                Destroy(gameObject);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            GameOver.Instance.GameEnded();
        }
    }
}


