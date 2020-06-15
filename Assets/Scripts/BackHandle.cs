using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHandle : MonoBehaviour
{
    [SerializeField] private Transform target = default;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {
        rb.MovePosition(target.position);
    }
}
