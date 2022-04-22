using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHitDetect : MonoBehaviour
{
    [SerializeField] MinionHealth minionHealth;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "M" || other.tag == "Terrain")
        {
            minionHealth.Health = 0;
        }
    }
}
