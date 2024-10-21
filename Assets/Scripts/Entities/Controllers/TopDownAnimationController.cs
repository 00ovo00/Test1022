using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TopDownAnimationController : AnimationController
{
    // Animator.StringToHash�� ���� Animator ���� ��ȯ�� Ȱ��Ǵ� �κп� ���� ����ȭ ����
    // StringToHash�� IsWalking�̶�� ���ڿ��� �Ϲ��� �Լ��� �ؽ��Լ��� ���� Ư���� ������ ��ȯ
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsInvincible = Animator.StringToHash("IsInvincible");

    private readonly float magnituteThreshold = 0.5f;   // ���� ��ȭ�� �ʿ��� �ּҰ�

    private HealthSystem healthSystem;
    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        healthSystem = GetComponent<HealthSystem>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        // �����ϰų� ������ �� �ִϸ��̼��� ���� �����ϵ��� ����
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityStart += InvincibilityStart; 
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > magnituteThreshold);
    }

    // OnAttackEvent�� Action<AttackSO>�̱� ������ Attacking�� AttackSO�� ������� �ʾƵ� �Ű������� ������ �־�� �մϴ�.
    // �̷� �� �Լ�(�޼ҵ�) �ñ״�ó�� ����ٶ�� �մϴ�.
    private void Attacking(AttackSO obj)
    {
        animator.SetTrigger(Attack);
    }

    private void Hit()
    {
        if (!animator.GetBool(IsInvincible))
        {
            animator.SetBool(IsHit, true);
        }
    }

    private void Die()
    {
        animator.SetTrigger(Dead);
    }
    private void InvincibilityStart(float duration)
    {
        animator.SetBool(IsInvincible, true); 
    }

    // ���� ���� ����
    private void InvincibilityEnd()
    {
        animator.SetBool(IsInvincible, false);
        animator.SetBool(IsHit, false);
    }
}