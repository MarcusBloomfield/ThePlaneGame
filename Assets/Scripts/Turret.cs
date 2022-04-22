using UnityEngine;
using UnityEngine.PlayerLoop;

public class Turret  : MonoBehaviour
{
    [SerializeField] GameObject Gun;
    GameObject Target;
    [SerializeField] GameObject Laser;
    [SerializeField] float FireRate = .5f;
    float timer;

    public AudioSource audioSource;

    public GameObject SetTarget {set => Target = value; }

    public virtual void Update()
    {
        TurretFunction();
    }
    public void TurretFunction()
    {
        if (Target != null)
        {
            Gun.transform.LookAt(Target.transform.position);
            timer += Time.deltaTime;
            if (timer >= FireRate)
            {
                Instantiate(Laser, Gun.transform.position, Gun.transform.rotation);
                PlayLazerSound();
                timer = 0;
            }
        }
    }
    public virtual void PlayLazerSound()
    {
        AudioManager.Instance.PlaySound(audioSource, Sound.AllSoundsTypes.Lazer);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Target = other.gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Target = null;
        }
    }
}
