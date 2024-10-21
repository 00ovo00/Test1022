using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyController : TopDownController
{
    GameManager gameManager;
    protected Transform ClosestTarget { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player.transform;
        Debug.Log(gameManager.Player);
    }

    protected virtual void FixedUpdate()
    {
        // ���� �̵� ������Ʈ ó�� (��ӹ��� Ŭ�������� ����)
    }

    protected float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
}