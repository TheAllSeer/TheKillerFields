using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (animator == null)
        {
            animator.GetComponentInChildren<Animator>();
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        Destroy(gameObject, 1f);
    }
}
