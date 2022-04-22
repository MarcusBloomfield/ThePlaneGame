using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerHitDetect : MonoBehaviour
{

    [SerializeField] PlayerJet playerJet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            GameManager.Instance.Reload();
        }
    }
}
