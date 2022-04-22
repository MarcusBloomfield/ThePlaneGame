using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    [SerializeField] GameObject playerJet;
    [SerializeField] AudioSource DeathSource;
    bool hasPlayed = false;
    private void Update()
    {
        gameObject.transform.LookAt(playerJet.transform.position);
        if (hasPlayed == false)
        {
            AudioManager.Instance.PlaySound(DeathSource, Sound.AllSoundsTypes.DEATH);
            hasPlayed = true;
        }
    }
}
