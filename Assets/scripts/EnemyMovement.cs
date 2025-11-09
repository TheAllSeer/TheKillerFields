using UnityEngine;

public class EnemyMovement : MonoBehaviour
{



    public Rigidbody2D rb;
    [Header("Movement")]
    public float moveSpeed = 1.5f;
    bool isFacingRight = false;
    float horizontalMovement;
    float verticalMovement;
    bool isMoving;

    [SerializeField] private Animator animator;

    [Header("Player Tracking")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float stopDistance = 1.2f;

    [Header("Attacks")]
    bool isAttacking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                Attack();
            }
            else if (distanceToPlayer > stopDistance && !isAttacking)
            {
                MoveTowardsPlayer();
            }
            else if (!isAttacking)
            {
                StopMoving();
            }
        }
        else
        {
            StopMoving();
        }
        animator.SetBool("isSkeletonRunning", isMoving);


        if (!isAttacking)
        {
            Flip();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        horizontalMovement = direction.x;
        verticalMovement = direction.y;

        rb.linearVelocity = direction * moveSpeed;
        isMoving = true;
    }
    void StopMoving()
    {
        horizontalMovement = 0;
        verticalMovement = 0;
        rb.linearVelocity = Vector2.zero;
        isMoving = false;
    }
    void Attack()
    {
        StopMoving();
        isAttacking = true;
        animator.SetTrigger("skeletonAttackStarted");
    }
    public void AttackFinished()
    {
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player_hitbox"))
        {
            isAttacking = false;
            animator.ResetTrigger("skeletonAttackStarted");
            animator.SetTrigger("isGettingHit");
            // Take damage
            // Trigger hit animation
            // etc.
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
