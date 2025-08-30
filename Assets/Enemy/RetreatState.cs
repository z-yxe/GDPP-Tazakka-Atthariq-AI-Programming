using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(EnemyController enemy) 
    {
        Debug.Log("Start Retreatting");
    }

    public void UpdateState(EnemyController enemy) 
    {
        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.transform.position - enemy.player.transform.position;
        }
    }

    public void ExitState(EnemyController enemy)
    {
        Debug.Log("Stop Retreatting");
    }
}
