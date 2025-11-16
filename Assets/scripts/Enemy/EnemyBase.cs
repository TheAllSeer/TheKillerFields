using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform player;

    bool isFacingRight = false;
    bool isDead = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }
    void Update()
    {
        if (isDead) return;
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed; // check this, gpt said "rb.velocity".
        if ((dir.x > 0 && !isFacingRight) || (dir.x < 0 && isFacingRight))
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0); // this is different than so far, but looks simpler. ask about this. 
    }
    void OnDeath()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Die");
        Destroy(gameObject, 1.2f);
    }
}