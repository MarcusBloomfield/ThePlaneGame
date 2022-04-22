using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitDetect : MonoBehaviour
{
    [SerializeField] BossHealth bOSSTurret;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "M")
        {
            bOSSTurret.Health--;
        }
    }
}
