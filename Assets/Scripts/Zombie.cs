using System;
using Pathfinding;
using UnityEngine;

// using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    private enum State
    {
        Idle,
        Moving,
        Attack,
        Patrol,
    }

    [Header("Movement")]
    [SerializeField] private float moveRadius = 10;
    [SerializeField] private float attackRadius = 3;
    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackRate;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string shootTriggerName;

    [SerializeField] private LayerMask obstacleMask;
    
    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float minDist;
    
    
    

    private Player player;
    public Transform playerTransform { get; private set; }
    private Transform cachedTransform;
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;
    private float lastAttackTime;

    private Transform startPositionTransform;

[SerializeField]
    private State currentState;

    private int patrolIndex = -1;
    private Transform patrolTransform;

    private bool needPatrol;

    private void Awake()
    {
        startPositionTransform = new GameObject($"ololo {name}").transform;
        startPositionTransform.position = transform.position;
        
        cachedTransform = transform;
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();

        needPatrol = patrolPoints != null && patrolPoints.Length > 1;
        SetPatrolOrIdle();
    }

    private void SetPatrolOrIdle()
    {
        SetState(needPatrol? State.Patrol : State.Idle);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }

    private void Update()
    {
        CheckNewState();
        UpdateCurrentState();
    }

    private void SetState(State newState)
    {
        switch (newState)
        {
            case State.Idle:
            {
                if (currentState == State.Moving)
                {
                    SetActiveMovement(false);
                }

                break;
            }
            case State.Moving:
            {
                if (currentState == State.Idle || currentState == State.Attack)
                {
                    SetActiveMovement(true);
                    SetTarget(playerTransform);
                }

                break;
            }
            case State.Attack:
            {
                if (currentState == State.Moving)
                {
                    SetActiveMovement(false);
                }

                break;
            }
            case State.Patrol:
            {
                SetActiveMovement(true);
                UpdatePatrolPoint();
                SetPatrolPointAsTarget();
                
                break;
                
            }
        }

        currentState = newState;
    }

    private void UpdatePatrolPoint()
    {
        patrolIndex++;

        if (patrolIndex >= patrolPoints.Length)
        {
            patrolIndex = 0;
        }

        patrolTransform = patrolPoints[patrolIndex];
    }

    private void SetPatrolPointAsTarget()
    {
        SetTarget(patrolTransform);
    }

    private void CheckNewState()
    {
        var playerPos = playerTransform.position;
        var distance = Vector3.Distance(playerPos, cachedTransform.position);

        if (distance > moveRadius && currentState != State.Patrol)
        {
            SetPatrolOrIdle();
        }
        else if (distance < attackRadius)
        {
            SetState(State.Attack);
        }
        else
        {
            // Handle moving
            var direction = playerPos - transform.position;
            Debug.DrawRay(transform.position, direction, Color.red);

            var ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, obstacleMask);

            if (ray.collider != null)
            {
                return;
            }
            
            SetState(State.Moving);
        }
    }

    private void UpdateCurrentState()
    {
        if (currentState == State.Attack)
        {
            Attack();
        }
        else if (currentState == State.Moving)
        {
            animator.SetFloat("MoveSpeed", aiPath.velocity.magnitude);
        }
        else if (currentState == State.Patrol)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        var dist = Vector3.Distance(transform.position, patrolTransform.position);
        Debug.LogError($"Patrol dist <{dist}>");
        if (dist <= minDist)
        {
            UpdatePatrolPoint();
            SetPatrolPointAsTarget();
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime > attackRate)
        {
            animator.SetTrigger(shootTriggerName);
            player.ChangeHealth(-damage);
            lastAttackTime = Time.time;
        }
    }

    private void SetActiveMovement(bool isActive)
    {
        aiPath.enabled = isActive;
    }

    private void SetTarget(Transform target)
    {
        aiDestinationSetter.target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
