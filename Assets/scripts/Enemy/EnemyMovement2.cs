using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    [Header("Target")]
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float stopDistance = 1.2f;

    [Header("Animator")]
    public Animator animator;
    private bool isFacingRight = true;

    [Header("States")]
    private bool isAttacking = false;
    private bool isHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        // Runtime assignment of the scene player
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
            else
                Debug.LogError("EnemyMovement2: No player found in the scene with tag 'Player'");
        }
    }

    private void Update()
    {
        if (player == null || isHit) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Attack
            if (distance <= attackRange && !isAttacking)
            {
                StartAttack();
            }
            // Move
            else if (distance > stopDistance && !isAttacking)
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

        // Animator
        if (animator != null)
            animator.SetBool("isMoving", rb.linearVelocity.sqrMagnitude > 0);

        // Flip
        if (!isAttacking)
            FlipTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player == null) return;

        moveDirection = (player.position - transform.position).normalized;
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
        moveDirection = Vector2.zero;
    }

    private void FlipTowardsPlayer()
    {
        if (player == null) return;

        bool shouldFaceRight = player.position.x > transform.position.x;
        if (isFacingRight != shouldFaceRight)
        {
            isFacingRight = shouldFaceRight;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (isFacingRight ? 1 : -1);
            transform.localScale = scale;
        }
    }

    private void StartAttack()
    {
        StopMoving();
        isAttacking = true;

        if (animator != null)
            animator.SetTrigger("Attack"); // Make sure this matches your attack trigger

        // Optional: enable attack hitbox via EnemyAttackController
    }

    // Called via animation events
    public void OnAttackFinished() => isAttacking = false;
    public void OnHitFinished() => isHit = false;
    public void OnDeathAnimationComplete() => Destroy(gameObject, 1f);
}
