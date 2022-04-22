using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum AllSoundsTypes {Boop, Lazer, TargetLocked, EnemyMissile , MissileLaunch , DEATH, BossDefeated, BossLazer, HighPitchEnemyMissileAlert}
    public AllSoundsTypes ActualSound;

    public AudioClip audioClip;

    public float Volume;

    public float Pitch;

    public bool Loop;

    public float Spatial;
}
