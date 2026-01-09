using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public List<Transform> waypoints;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public PlayerController player;

    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public RetreatState retreatState = new RetreatState();

    public float healthPoint = 50f;

    private BaseState currentState;
    public float chaseDistance;

    public void Setup (List<Transform> waypoints, PlayerController player)
    {
        this.waypoints = waypoints;
        this.player = player;
    }

    private void Awake()
    {
        currentState = patrolState;
        currentState.EnterState(this);
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

    public void TakeDamage(float damage)
    {
        healthPoint -= damage;

        if (healthPoint <= 0)
        {
            gameObject.SetActive(false);
        }
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
