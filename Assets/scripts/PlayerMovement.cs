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
    [SerializeField] private Animator animator;

    void Start()
    {

    }

    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
        Flip();
        // rb.linearVelocity = new Vector2(verticalMovement * moveSpeed, rb.linearVelocityY);
        bool isMoving = horizontalMovement != 0 || verticalMovement != 0;
        animator.SetBool("isRunning", isMoving);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        horizontalMovement = moveInput.x;
        verticalMovement = moveInput.y;
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