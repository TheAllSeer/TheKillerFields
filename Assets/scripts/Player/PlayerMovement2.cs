using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    Vector2 moveInput;

    [Header("Animation")]
    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (animator != null)
        {
            bool isMoving = moveInput.sqrMagnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}