using UnityEngine;

public interface BaseState
{
    public void EnterState(EnemyController enemy);
    public void UpdateState(EnemyController enemy);
    public void ExitState(EnemyController enemy);
}
