using UnityEngine;

public class PatrolState : BaseState
{
    private bool isMoving;
    private Vector3 destination;

    public void EnterState(EnemyController enemy)
    {
        isMoving = false;
        enemy.animator.SetTrigger("PatrolState");
    }

    public void UpdateState(EnemyController enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.chaseDistance)
        {
            enemy.SwitchState(enemy.chaseState);
        }

        if (!isMoving)
        {
            isMoving = true;
            if (enemy.waypoints.Count > 0) 
            {
                int index = Random.Range(0, enemy.waypoints.Count);
                destination = enemy.waypoints[index].position;
                enemy.navMeshAgent.destination = destination;
            }
        }
        else
        {
            if (!enemy.navMeshAgent.pathPending && 
                enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance + 0.5f)
            {
                isMoving = false;
            }
        }
    }

    public void ExitState(EnemyController enemy)
    {
        // logic if there is any mechanic in stop patrol condition
    }
}
