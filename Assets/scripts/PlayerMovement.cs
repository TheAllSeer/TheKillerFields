using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 3f;

    // bool isFacingRight = true;
    float horizontalMovement;
    float verticalMovement;

    void Start()
    {

    }

    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
        // rb.linearVelocity = new Vector2(verticalMovement * moveSpeed, rb.linearVelocityY);

    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        horizontalMovement = moveInput.x;
        verticalMovement = moveInput.y;
    }


}