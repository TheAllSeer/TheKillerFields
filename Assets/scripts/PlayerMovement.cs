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

    void Start()
    {

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
    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0){
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }


}