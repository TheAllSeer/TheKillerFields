using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 3f;

    bool isFacingRight = true;
    float horizontalMovement;
    float verticalMovement;
    bool isMoving;
    [SerializeField] private Animator animator;


    [Header("Attacks")]
    public InputAction playerAttack;
    bool isAttacking;
    [Header("Combo Attack")]
    private bool isCombo = false;
    private bool isGCD = false;

    [Header("Health")]
    public int maxHealth = 5;
    int currentHealth;
    bool isDead = false;



    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isAttacking)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
            Flip();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        isMoving = (horizontalMovement != 0 || verticalMovement != 0) && !isAttacking;
        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isCombo1", isCombo);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        horizontalMovement = moveInput.x;
        verticalMovement = moveInput.y;

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
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isCombo)
            {
                animator.SetTrigger("isAttacking2");
            }
            else if (!isGCD)
            {
                animator.SetTrigger("isAttacking1");
            }
            isAttacking = true;
            isMoving = false;
        }
    }
    public void Attack1Finished()
    {
        isAttacking = false;
        animator.ResetTrigger("isAttacking1");
        animator.ResetTrigger("isAttacking2");
    }

    // combo!
    public void Combo1WindowOpen()
    {
        isCombo = true;
    }
    public void Combo1WindowClosed()
    {
        isCombo = false;
    }

    public void StartGCD()
    {
        isGCD = true;
    }
    public void EndGCD()
    {
        isGCD = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy_hitbox"))
        {
            TakeDamage(1);
        }
    }
    public void HitFinished()
    {
        isMoving = horizontalMovement != 0 || verticalMovement != 0;
        animator.SetBool("isRunning", isMoving);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        Vector3 offset = new Vector3(
            UnityEngine.Random.Range(-0.3f, 0.3f),
            0.05f,
            0
        );
        FloatingTextManager.Instance.ShowDamage(damage, transform.position + offset);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        isAttacking = false;
        isCombo = false;
        isGCD = false;
        animator.ResetTrigger("isAttacking1");
        animator.ResetTrigger("isAttacking2");
        animator.SetTrigger("isGettingHit");

    }

    private void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("isDead");
        foreach (var collider in GetComponentsInChildren<Collider2D>())
            collider.enabled = false;
        this.enabled = false;
    }
    public void OnDeathAnimationComplete()
    {
        Destroy(gameObject, 1f);
    }
    


}

// things i think would be fun to add:
// * if his movement speed increased like lillia from league, the more attacks he hits the higher his attack speed
//   up to 4, stacks are lost when not attacking for a while
// * when getting upgrades, have some sort of a visual on the character. for example - 
//   if my upgrade is adding red lightnings that follow the crows i shoot out, 
//   have the character also have red lightnings around it, so that the player knows this power up is active.


