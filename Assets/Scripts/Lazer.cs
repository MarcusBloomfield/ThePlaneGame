
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] float LazerSpeed;

    private void Start()
    {
        RB.velocity = gameObject.transform.forward * LazerSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 0 && other.gameObject.layer != 8)
        {
            Destroy(gameObject, 2);
        }
    }
}
