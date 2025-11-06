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
    bool isAttacking1;
    void Start()
    {

    }

    void Update()
    {
        if (!isAttacking1)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        Flip();
        isMoving = (horizontalMovement != 0 || verticalMovement != 0) && !isAttacking1;
        animator.SetBool("isRunning", isMoving);
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
            animator.SetTrigger("isAttacking1");
            isAttacking1 = true;
            isMoving = false;
        }
    }
    public void Attack1Finished()
    {
        isAttacking1 = false;
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