using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject TargettedRetical;
    [SerializeField] Rigidbody rigidbody;
    bool ooops = false;

    private void Update()
    {
        if (GameManager.Instance.PlayerJet.Target == gameObject)
        {
            TargettedRetical.SetActive(true);
        }
        else
        {
            TargettedRetical.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "M" || other.tag == "P")
        {
            GameManager.Instance.Score1++;
            if (GameManager.Instance.PlayerJet != null)
            {
                AudioManager.Instance.PlayOneShot(GameManager.Instance.MissileAlertSource, Sound.AllSoundsTypes.Boop);
            }
            GameManager.Instance.SpawnTarget();
            if (ooops == true)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void StartBossExplosion()
    {
        StartCoroutine(BossExplosion());
    }
    IEnumerator BossExplosion()
    {
        ooops = true;
        yield return new WaitForSeconds(2);
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}