using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(EnemyController enemy) 
    {
        enemy.animator.SetTrigger("RetreatState");
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
        // logic if there is any mechanic in stop retreat condition
    }
}
