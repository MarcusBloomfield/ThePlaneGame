using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileDetection : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    List<GameObject> EnemyMissiles = new List<GameObject>();
    [SerializeField] Sound.AllSoundsTypes WantedSound;
    private void Update()
    {
        PlayAlert();
        DisplayAlert();
        CheckForNullObjs();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "E")
        {
            EnemyMissiles.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (EnemyMissiles.Contains(other.gameObject))
        {
            EnemyMissiles.Remove(other.gameObject);
        }
    }
    void PlayAlert()
    {
        if (audioSource.isPlaying == false && EnemyMissiles.Count > 0)
        {
            AudioManager.Instance.PlaySound(audioSource, WantedSound);
        }
    }
    void DisplayAlert()
    {
        if (audioSource.isPlaying == true)
        {
            UIManager.Instance.DisplayMissileAlert(true);
        }
        else
        {
            UIManager.Instance.DisplayMissileAlert(false);
        }
    }
    void CheckForNullObjs()
    {
        if (EnemyMissiles.Count > 0)
        {
            foreach (GameObject item in EnemyMissiles)
            {
                if (item == null)
                {
                    EnemyMissiles.Remove(item);
                }
                else if (item.tag != "E")
                {
                    EnemyMissiles.Remove(item);
                }
            }
        }
    }
}
