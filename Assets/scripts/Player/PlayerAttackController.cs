using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    [Header("Attack Settings")]
    public float globalCooldown = 0.2f;
    private float nextAttackTime = 0f;

    //combo
    private int currentComboIndex = 0;
    private float comboResetTimer = 0f;
    public float comboMaxDelay = 0.6f;

    private void Update()
    {
        HandleInput();
        HandleComboReset();
    }

    private void HandleInput()
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            PlayComboAttack();
        }
    }
    private void PlayComboAttack()
    {
        animator.SetInteger("ComboIndex", currentComboIndex);
        animator.SetTrigger("Attack");
        nextAttackTime = Time.time + globalCooldown;

        currentComboIndex++;
        comboResetTimer = Time.time;
    }
    private void HandleComboReset()
    {
        if (currentComboIndex > 0 && Time.time > comboResetTimer + comboMaxDelay)
        {
            currentComboIndex = 0;
        }
    }
    public void ResetCombo()
    {
        currentComboIndex = 0;
    }
}