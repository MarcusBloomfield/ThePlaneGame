using UnityEngine;

public class MissileTurret : MonoBehaviour
{
    [SerializeField] GameObject Gun;
    GameObject Target;
    [SerializeField] GameObject Missile;
    [SerializeField] GameObject LaserSpawn;
    [SerializeField] float FireRate = 15;
    float timer = 0;
    [SerializeField] AudioSource audioSource;
    
    private void Update()
    {
        if (Target != null)
        {
            Gun.transform.LookAt(Target.transform.position);
            timer += Time.deltaTime;
            if (timer >= FireRate)
            {
                Instantiate(Missile, LaserSpawn.transform.position, LaserSpawn.transform.rotation, gameObject.transform).GetComponent<Missile>().Target = Target;
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
