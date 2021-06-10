using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private string moveSpeedName;


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.velocity = direction * speed;

        animator.SetFloat(moveSpeedName, direction.magnitude);
    }

    private void Rotate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePosition - transform.position);
        
        transform.up = -((Vector2) direction);
    }
}
