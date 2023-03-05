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


        private void OnEnable()
        {
            pos = transform.position;
            GenerateObstacles();
        }

        private void OnDisable()
        {
            for (int i = 0; i < range; i++)
            {
                Destroy(obsPool[i]);
            }
        }


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

