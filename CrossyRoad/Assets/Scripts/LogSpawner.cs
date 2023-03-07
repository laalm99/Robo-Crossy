using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lamya.CrossyRoad
{
    public class LogSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject obstaclePrefab;
        private float spawnRate;
        private float direction;
        private int toggle;

        /// <summary>
        /// toggle takes a value between 0 and 1, to decide the direction of the spawner.
        /// </summary>
        private void OnEnable()
        {
            toggle = UnityEngine.Random.Range(0, 2);
            if (toggle == 0)
            {
                direction = 1;
            }
            else if (toggle == 1)
            {
                direction = -1;
            }
            spawnRate = UnityEngine.Random.Range(3f, 8f);
            transform.position = new Vector3(transform.position.x * -direction, transform.position.y, transform.position.z);
            InvokeRepeating(nameof(ObstacleSpawn), spawnRate, spawnRate);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(ObstacleSpawn));
        }

        private void ObstacleSpawn()
        {
            GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            obstacle.GetComponent<LogBehaviour>().Direction = direction;
        }

    }

}