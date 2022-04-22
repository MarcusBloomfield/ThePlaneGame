using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrashDetect : MonoBehaviour
{
    [SerializeField] BossHealth bOSSTurret;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            bOSSTurret.Health = 0;
        }
    }
}
