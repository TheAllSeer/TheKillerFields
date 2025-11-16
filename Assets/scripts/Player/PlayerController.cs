using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement2 movement;
    private Animator animator;
    private void Awake()
    {
        movement = GetComponent<PlayerMovement2>();
        animator = GetComponentInChildren<Animator>();
    }
}