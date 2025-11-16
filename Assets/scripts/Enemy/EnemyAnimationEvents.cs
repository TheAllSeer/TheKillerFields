using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public EnemyMovement2 enemy;

    public void OnAttackFinished()
    {
        if (enemy != null)
            enemy.OnAttackFinished();
    }

    public void OnHitFinished()
    {
        if (enemy != null)
            enemy.OnHitFinished();
    }

    public void OnDeathAnimationComplete()
    {
        if (enemy != null)
            enemy.OnDeathAnimationComplete();
    }
}
