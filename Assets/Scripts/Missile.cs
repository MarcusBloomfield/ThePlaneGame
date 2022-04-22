using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] TrailRenderer Trail;

    [SerializeField] float Speed = 50;
    [SerializeField] float Acceleration = 0.4f;
    [SerializeField] float TurnRate = 3;
    [SerializeField] float LifeTime = 30;

    public GameObject Target; 
    public AudioSource audioSource;
    bool HasFuel = true;
    bool isArmed = false;

    private void Start()
    {
        Trail.enabled = false;
        boxCollider.enabled = false;
    }
    private void Update()
    {
        MissileHoming();
    }
    public void MissileHoming()
    {
        if (Target != null && HasFuel == true && Target.activeSelf == true)
        {
            PlayMissileSound();
            rigidbody.isKinematic = false;
            gameObject.transform.parent = null;
            rigidbody.velocity = gameObject.transform.forward * Speed;
            Speed += Acceleration * Time.deltaTime;
            if (isArmed == false)
            {
                StartCoroutine(SafteyCheck());
                StartCoroutine(LifeTimer());
                isArmed = true;
            }
            Trail.enabled = true;
            rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), TurnRate));
        }
        if (Target != null && Target.activeSelf == false)
        {
            Dead();
        }
    }
    void PlayMissileSound()
    {
        if (audioSource.isPlaying == false)
        {
            AudioManager.Instance.PlaySound(audioSource, Sound.AllSoundsTypes.MissileLaunch);
        }
    }
    IEnumerator SafteyCheck()
    {
        yield return new WaitForSeconds(1);
        boxCollider.enabled = true;
    }
    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(LifeTime);
        HasFuel = false;
        Dead();
    }
    void Dead()
    {
        rigidbody.useGravity = true;
        Trail.enabled = false;
        audioSource.Stop();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
