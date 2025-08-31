using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator animator;
    public PlayerController player;

    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public RetreatState retreatState = new RetreatState();

    private BaseState currentState;
    public float chaseDistance;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        currentState = patrolState;
        currentState.EnterState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (player != null)
        {
            player.onPowerUpStart += StartRetreating;
            player.onPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    private void StartRetreating()
    {
        SwitchState(retreatState);
    }

    private void StopRetreating()
    {
        SwitchState(patrolState);
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentState != retreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().Dead();
            }
        }
    }
}
