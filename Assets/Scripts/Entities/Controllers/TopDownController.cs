using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    // ���� ��ü���� �̺�Ʈ �Լ� ��� �ֱ�
    // event Ű���� �Է��ϸ� public �̶� ���⼭�� Invoke ����
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    // OnAttackEvent�� ������ �� ���� ��������(AttackSO) ��� ��
    public event Action<AttackSO> OnAttackEvent;


    // ������ ���� �ð� ����
    // ������ ���� ������ ���ο� ���� �̺�Ʈ �߻� ���� �ð� ���ϱ� ����
    private float timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }    // ���� ���� �������� üũ

    // private set���� ���⼭�� �� ���� �����ϵ��� ����
    // protected�� ��� ���� Ŭ������ �����ϵ��� ����
    protected CharacterStatHandler stats {  get; private set; }

    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
    }

    protected virtual void Update()
    {
        HandleAttackDelay();
    }
    private void HandleAttackDelay()
    {
        // ���� �߻� ���� ���� �ð��� ����
        if (timeSinceLastAttack <= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && timeSinceLastAttack > stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0;
            // ���� ������ ������ attackSO ����
            CallAttackEvent(stats.CurrentStat.attackSO);
        }
    }

    // �̺�Ʈ �߻��� ��ϵ� �޼ҵ� ȣ��
    // OnXXX?.Invoke()�� null safe�ϰ� ���
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }
}
