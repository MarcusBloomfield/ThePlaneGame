using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetMissileRadar : MonoBehaviour
{
    [SerializeField] PlayerJet playerJet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target" && playerJet.Missiles[5] != null)
        {
            playerJet.RadarList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerJet.RadarList.Remove(other.gameObject);
    }
}
