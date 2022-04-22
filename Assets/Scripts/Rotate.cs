using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float SpinSpeed;
    private void Update()
    {
        gameObject.transform.Rotate(Vector3.up * SpinSpeed * Time.deltaTime);
    }
}
