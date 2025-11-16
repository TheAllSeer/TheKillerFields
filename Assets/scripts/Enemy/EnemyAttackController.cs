using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public Collider2D attackHitbox;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(damage);
        }
    }
}
