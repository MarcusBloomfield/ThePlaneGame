using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    [SerializeField] GameObject PointTargets;
    public int Health = 100;

    private void Update()
    {
        if (MinionIsDead())
        {
            MinionDie();
        }
    }
    bool MinionIsDead()
    {
        if (Health <= 0)
        {
            return true;
        }
        return false;
    }
    void MinionDie()
    {
        UIManager.Instance.SetBossEnabled(false);
        Instantiate(PointTargets, gameObject.transform.position, gameObject.transform.rotation).GetComponent<Target>().StartBossExplosion();
        gameObject.SetActive(false);
    }
}
