using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSTurret : Turret
{
    public override void PlayLazerSound()
    {
        AudioManager.Instance.PlayOneShot(audioSource, Sound.AllSoundsTypes.BossLazer);
    }
}