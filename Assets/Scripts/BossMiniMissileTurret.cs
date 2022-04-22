using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMiniMissileTurret : MonoBehaviour
{
    GameObject Target;
    [SerializeField] GameObject Missile;
    [SerializeField] GameObject MissileSpawn;
    [SerializeField] float FireRate = 15;
    float timer = 0;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        Target = PlayerJet.Instance.ActualJet;
    }
    private void Update()
    {
        if (Target != null)
        {
            MissileSpawn.transform.LookAt(Target.transform.position);
            timer += Time.deltaTime;
            if (timer >= FireRate)
            {
                Instantiate(Missile, MissileSpawn.transform.position, MissileSpawn.transform.rotation, gameObject.transform).GetComponent<Missile>().Target = Target;
                AudioManager.Instance.PlaySound(audioSource, Sound.AllSoundsTypes.MissileLaunch);
                timer = 0;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Target = other.gameObject;
        }
    }
}
