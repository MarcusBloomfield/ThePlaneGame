using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMotherShip : MonoBehaviour
{
    [SerializeField] GameObject Minion;
    [SerializeField] float SpawnRate = 3;
    [SerializeField] Transform SpawnPos;
    float Timer = 0;

    private void Update()
    {
        SpawnMinion();
    }
    void SpawnMinion()
    {
        Timer += 1 * Time.deltaTime;
        if (Timer > SpawnRate)
        {
            Instantiate(Minion, SpawnPos.position, SpawnPos.rotation);
            Timer = 0;
        }
    }
}
