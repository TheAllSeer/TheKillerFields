using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public Animator animator;

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (animator != null)
            animator.SetTrigger("Hit"); // new trigger

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        if (animator != null)
            animator.SetTrigger("Death"); // new trigger
        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f); // destroy after animation plays
    }
}
