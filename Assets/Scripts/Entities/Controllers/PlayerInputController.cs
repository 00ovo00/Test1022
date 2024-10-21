using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerInputController : TopDownController
{
    private Camera _camera;

    public event Action OnPlayerAttack;

    protected override void Awake()
    {
        base.Awake();   // �θ��� Awake�� ����
        _camera = Camera.main;  // ���� ī�޶� ��������
        GameManager.Instance.playerHealthSystem = this.GetComponent<HealthSystem>();
    }

    // OnXXX �޼ҵ�� ����� �Է� �����͸� ����ȭ ���� ��ó�� ��ġ��
    // ���� ��ü�� TopDownController�� ����
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;    // ������ �Է��� ����ȭ
        CallMoveEvent(moveInput);   // ����ȭ�� �Է� ���� �̺�Ʈ �޼ҵ�� ����
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();  // ���ο� ���콺 ���� ���� ��������
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);  // ��ũ���� ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        newAim = (worldPos - (Vector2)transform.position).normalized;   // ���� ��ǥ�� ���콺 ���� ����ȭ

        if (newAim.magnitude >= .9f)
        // Vector ���� �Ǽ��� ��ȯ
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        // ���콺 ��Ŭ�� �̺�Ʈ �߻��ϸ� �������� ���·� ����
        IsAttacking = value.isPressed;
        
    }

    public void CallAttack()
    {
        OnPlayerAttack?.Invoke();
    }
}