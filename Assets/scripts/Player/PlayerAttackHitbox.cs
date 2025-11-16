using UnityEngine;  

public class PlayerAttackHitbox : MonoBehaviour
{
    public int damage = 1;
    public LayerMask enemyLayers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1<<collision.gameObject.layer) & enemyLayers) != 0)
        {
            if (collision.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(damage)
            }
        }
    }

    public void EnableHitBox() => gameObject.SetActive(true); // add these as animation events in the swing animation's start and end.
    public void DisableHitBox() => gameObject.SetActive(false);
}