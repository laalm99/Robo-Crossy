using System.Collections;
using System.Collections.Generic;
using Lamya.CrossyRoad;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        GameOver.Instance.GameEnded();
    }
}
