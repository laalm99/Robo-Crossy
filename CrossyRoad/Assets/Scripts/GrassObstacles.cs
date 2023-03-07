using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lamya.CrossyRoad
{
    public class GrassObstacles : MonoBehaviour
    {
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private List<GameObject> obsPool = new List<GameObject>();
        private GameObject temp;
        private int range;
        Vector3 pos;

        /// <summary>
        /// Since this script is attached to the grass piece, if it's enabled pos will take it's position
        /// and GenerateObstacles will be called
        /// </summary>
        private void OnEnable()
        {
            pos = transform.position;
            GenerateObstacles();
        }
        /// <summary>
        /// Once the grass peice is disabled in the heirachy, the obsPool will be destroyed.
        /// This is because I want the obstacles postion and number to be random for each grass piece.
        /// </summary>
        private void OnDisable()
        {
            for (int i = 0; i < range; i++)
            {
                Destroy(obsPool[i]);
            }
        }

        /// <summary>
        /// Generates a random number of obstacles, each in a random place and adds them to the list obsPool
        /// </summary>
        void GenerateObstacles()
        {
            range = (int)Random.Range(5, 20);

            for (int i = 0; i < range; i++)
            {
                temp = Instantiate(obstaclePrefab, new Vector3(Random.Range(-30, 31), pos.y + 0.5f, pos.z), Quaternion.identity);
                obsPool.Add(temp);
            }
        }


    }

}

