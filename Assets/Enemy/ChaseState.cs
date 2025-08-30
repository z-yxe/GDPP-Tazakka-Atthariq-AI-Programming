using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(EnemyController enemy) 
    {
        Debug.Log("Start Chasing");
    }

    public void UpdateState(EnemyController enemy) 
    {
        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.player.transform.position;

            if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.chaseDistance)
            {
                enemy.SwitchState(enemy.patrolState);
            }
        }
    }

    public void ExitState(EnemyController enemy)
    {
        Debug.Log("Stop Chasing");
    }
}
