using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRadar : MonoBehaviour
{
    [SerializeField] PlayerJet playerJet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target" || other.gameObject.tag == "Boss")
        {
            playerJet.GunRadarList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerJet.GunRadarList.Remove(other.gameObject);
    }
}
