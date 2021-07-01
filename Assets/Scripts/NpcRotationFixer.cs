using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NpcRotationFixer : MonoBehaviour
{
    public bool needRotate;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (!needRotate)
        {
            return;
        }

        transform.up = rb.velocity;
    }
}
