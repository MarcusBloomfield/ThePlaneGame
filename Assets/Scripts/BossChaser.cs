using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaser : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float TurnRate = 1;
    float Speed = 0;
    GameObject Target;
    private void Start()
    {
        Target = PlayerJet.Instance.gameObject;
    }
    private void Update()
    {
        MoveTOTarget();
        Speed = PlayerJet.Instance.Speed;
    }
    void MoveTOTarget()
    {
        if (Target != null)
        {
            rigidbody.velocity = gameObject.transform.forward * Speed;
            rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), TurnRate));
        }
    }
}
