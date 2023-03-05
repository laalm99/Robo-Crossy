using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lamya.CrossyRoad
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject obstaclePrefab;
        private float spawnRate;


        private void OnEnable()
        {
            spawnRate = UnityEngine.Random.Range(3f, 8f);
            InvokeRepeating(nameof(ObstacleSpawn), spawnRate, spawnRate);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(ObstacleSpawn));
        }

        private void ObstacleSpawn()
        {
            GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        }

    }

}
