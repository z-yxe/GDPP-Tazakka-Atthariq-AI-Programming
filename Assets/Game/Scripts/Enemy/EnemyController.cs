using System.Collections.Generic;
using Unity.Mathematics;
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

    [SerializeField] private GameObject bleedVfx;
    [SerializeField] private EnemyHealthUI healthUi;
    [SerializeField] private float healthPoint = 50f;
    private float maxHealth;

    private BaseState currentState;
    public float chaseDistance;

    public void Setup (List<Transform> waypoints, PlayerController player)
    {
        this.waypoints = waypoints;
        this.player = player;
    }

    private void Awake()
    {
        maxHealth = healthPoint;
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

    private void OnDisable()
    {
        player.onPowerUpStart -= StartRetreating;
        player.onPowerUpStop -= StopRetreating;
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
        healthUi.UpdateHealthBar(healthPoint, maxHealth);

        GameObject bloodInstance = Instantiate(bleedVfx, transform.position, Quaternion.identity);
        Destroy(bloodInstance, 2f);

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
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
