using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    public Sound[] sounds;

    public AudioSource audioManager;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void PlaySound(AudioSource audioSource, Sound.AllSoundsTypes type)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ActualSound == type)
            {
                SetClipSettingsToAudiosource(audioSource, i);

                audioSource.Play();
                
            }
        }
    }
    public void PlayOneShot(AudioSource audioSource, Sound.AllSoundsTypes type)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ActualSound == type)
            {
                SetClipSettingsToAudiosource(audioSource, i);
                audioSource.PlayOneShot(sounds[i].audioClip);

            }
        }
    }
    void SetClipSettingsToAudiosource(AudioSource audioSource, int i)
    {
        audioSource.volume = sounds[i].Volume;
        audioSource.pitch = sounds[i].Pitch;
        audioSource.spatialBlend = sounds[i].Spatial;
        audioSource.loop = sounds[i].Loop;
        audioSource.clip = sounds[i].audioClip;
    }
}
