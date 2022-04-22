using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int Health = 100;
    [SerializeField] GameObject PointTargets;
    private void Update()
    {
        if (!BossIsDead())
        {
            UIManager.Instance.SetBossEnabled(true);
        }
        else
        {
            BossDie();
        }
    }
    bool BossIsDead()
    {
        if (Health <= 0)
        {
            return true;
        }
        return false;
    }
    void BossDie()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.audioManager, Sound.AllSoundsTypes.BossDefeated);
        UIManager.Instance.SetBossEnabled(false);
        for (int i = 0; i < 25; i++)
        {
            Instantiate(PointTargets, gameObject.transform.position, gameObject.transform.rotation).GetComponent<Target>().StartBossExplosion();
        }
        gameObject.SetActive(false);
    }
}
