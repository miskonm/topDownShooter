using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private string moveSpeedName;
    [SerializeField] private float startDelay;
    
    private List<Vector3> playersPositions = new List<Vector3>();
    private bool canMove;
    
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector3 direction;
    private Transform cachedTransform;
    

    private void Awake()
    {
        cachedTransform = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;

        // StartCoroutine(CanMoveDelay());
    }
    

    private void OnDisable()
    {
        // rb.velocity = Vector2.zero;
        rb.Sleep();
        SetMoveAnimation(0);
    }

    private void Update()
    {
        // CheckPlayerPosition();

        // if (!canMove)
        // {
        //     return;
        // }
        
        Rotate();
        Move();
    }

    private void Move()
    {
        rb.velocity = direction * speed;

        SetMoveAnimation(direction.magnitude);
    }

    private void SetMoveAnimation(float magnitude)
    {
        animator.SetFloat(moveSpeedName, magnitude);
    }

    private void Rotate()
    {
        Vector3 playerPosition;
        
        // if (playersPositions.Count > 0)
        // {
        //     playerPosition = playersPositions[0];
        //     playersPositions.RemoveAt(0);
        // }
        // else
        // {
            playerPosition = playerTransform.position;
        // }

        direction = Vector3.ClampMagnitude(playerPosition - cachedTransform.position, 1f);

        cachedTransform.up = -((Vector2) direction);
    }

    private void CheckPlayerPosition()
    {
        playersPositions.Add(playerTransform.position);
    }

    private IEnumerator CanMoveDelay()
    {
        canMove = false;
        
        yield return new WaitForSeconds(startDelay);

        canMove = true;
    }
}
