using UnityEngine;

public class Health: MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;

    bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;

        // this is for floating text
        Vector3 offset = new Vector3(
            UnityEngine.Random.Range(-0.3f, 0.3f),
            0.05f,
            0
        );   
        FloatingTextManager.Instance.ShowDamage(amount, transform.position + offset);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
    }
}