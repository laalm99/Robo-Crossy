using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lamya.CrossyRoad
{
    public class TerrainSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] terrains = new GameObject[3];
        [SerializeField] private List<GameObject> terrainsPool = new List<GameObject>();
        [SerializeField] private Transform playerPos;
        private GameObject temp;
        private Vector3 nextTerrainPos;
        private Vector3 tempPos;
        private float tZ;

        void Start()
        {
            tZ = terrains[0].transform.localScale.z;
            nextTerrainPos = new Vector3(0, -0.75f, 0 + tZ);
            GenerateTerrain();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                RemoveTerrain();
                SpawnTerrain();
            }

        }

        /// <summary>
        /// Genrates the initial pool of terrains randomally from the prexisting array.
        /// "nextTerrainPos" is the position of the next terrain to be instantiated or activated, "tZ" is the terrain's z scale.
        /// </summary>
        private void GenerateTerrain()
        {
            for (int i = 0; i < 25; i++)
            {
                temp = Instantiate(terrains[Random.Range(0, 3)], nextTerrainPos, Quaternion.identity);
                nextTerrainPos = new Vector3(nextTerrainPos.x, nextTerrainPos.y, nextTerrainPos.z + tZ);
                terrainsPool.Add(temp);

            }
        }

        /// <summary>
        /// "diff" is the difference between the player's position and the terrain's position.
        /// if that difference is >= than tZ*3 that means the terrain piece is completely off camera and we should deactivate it.
        /// </summary>
        private void RemoveTerrain()
        {
            for (int i = 0; i < 25; i++)
            {
                tempPos = terrainsPool[i].transform.position;
                int diff = (int)(playerPos.position.z - tempPos.z);
                if (diff >= (tZ * 3))
                {
                    terrainsPool[i].SetActive(false);
                }
            }
        }

        /// <summary>
        /// "diff" is the difference between the next terrain's position and the player's position.
        /// if the difference is >= tZ*3 that means the next terrain's position will be within the view of the camera and a piece should be there
        /// we take a random temporary piece from the pool and check if it's inactive in the heirarchy, if true we place it in the
        /// next terrain's position and update it for the next position possible.
        /// </summary>
        private void SpawnTerrain()
        {
            int diff = (int)(nextTerrainPos.z - playerPos.position.z);
            if (diff >= (tZ * 3))
            {
                GameObject temp = terrainsPool[Random.Range(0, terrainsPool.Count)];
                if (!temp.activeInHierarchy)
                {
                    temp.transform.position = new Vector3(nextTerrainPos.x, nextTerrainPos.y, nextTerrainPos.z);
                    temp.SetActive(true);
                    nextTerrainPos = new Vector3(nextTerrainPos.x, nextTerrainPos.y, nextTerrainPos.z + tZ);
                }
            }
        }
    }

}

