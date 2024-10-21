using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController movementController;   // ���� ��ü
    private CharacterStatHandler characterStatHandler;
    private Rigidbody2D movementRigidbody;
    // �����ӿ� �ʿ��� �� ��ũ��Ʈ �� �����ϰ� RigidBody ������Ʈ �ʿ�
    // ������ �̵��� �Ͼ�� ���� ��ü ����

    private Vector2 movementDirection = Vector2.zero;
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration  = 0.0f;

    private void Awake()
    {
        // Awake�� �ַ� �ڽ��� ������Ʈ���� �Ͼ�� ���� ó��
        movementController = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        // OnMoveEvent�� Move�� ȣ���϶�� ���
        movementController.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        // ���� ������Ʈ���� ������ ����
        ApplyMovement(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }    
    }

    private void Move(Vector2 direction)
    {
        // �̵����⸸ ���صΰ� ������ ���������� ����.
        // �����̴� ���� ���� ������Ʈ���� ����(rigidbody�� �����ϱ�)
        movementDirection = direction;
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void ApplyMovement(Vector2 direction)
    {
        // ���� ������ ����
        direction = direction * characterStatHandler.CurrentStat.speed;
        if (knockbackDuration > 0.0f)
        {
            direction += knockback;
        }
        movementRigidbody.velocity = direction;
    }
}