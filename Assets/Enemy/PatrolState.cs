using UnityEngine;

public class PatrolState : BaseState
{
    private bool isMoving;
    private Vector3 destination;

    public void EnterState(EnemyController enemy)
    {
        isMoving = false;
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
            int index = Random.Range(0, enemy.waypoints.Count);
            destination = enemy.waypoints[index].position;
            enemy.navMeshAgent.destination = destination;
        }
        else
        {

            if (Vector3.Distance(destination, enemy.transform.position) <= 0.1)
            {
                isMoving = false;
            }
        }
    }

    public void ExitState(EnemyController enemy)
    {
        Debug.Log("Stop Patrolling");
    }
}
