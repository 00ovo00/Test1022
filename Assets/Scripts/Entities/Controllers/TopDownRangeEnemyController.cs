using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField] private float followRange = 15f;   // ���� ����
    [SerializeField] private float shootRange = 10f;    // ���� ����
    private int layerMaskLevel;     // �� �浹 ���̾�
    private int layerMaskTarget;    // Ÿ�� �浹 ���̾�

    protected override void Start()
    {
        base.Start();
        layerMaskLevel = LayerMask.NameToLayer("Level");
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);
    }

    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false; // �⺻������ ���� ���¸� false�� ����

        // �������� ���� ������ ��ó�� �ִ��� Ȯ��
        if (distance <= followRange)
        {
            CheckIfNear(distance, direction);
        }
    }

    private void CheckIfNear(float distance, Vector2 direction)
    {
        // Ÿ���� ���� ���� ���� ������ ����
        if (distance <= shootRange)
        {
            TryShootAtTarget(direction);
        }
        // Ÿ���� ���� ���� �ܿ� �ְ� ���� ���� ���� ������ �߰�
        else
        {
            CallMoveEvent(direction);
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        // ���� ��ġ���� direction �������� ���� �߻�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, GetLayerMaskForRaycast());

        // ���� ������ �ƴ϶� ���� �÷��̾ �¾Ҵ��� Ȯ��
        if (IsTargetHit(hit))
        {
            PerformAttackAction(direction);
        }
        else
        {
            CallMoveEvent(direction);// Ÿ���� ������ ���ϸ� �߰�
        }
    }

    private int GetLayerMaskForRaycast()
    {
        // "Level" ���̾�� Ÿ�� ���̾� ��θ� �����ϴ� LayerMask�� ��ȯ
        return (1 << layerMaskLevel) | layerMaskTarget;
    }

    private bool IsTargetHit(RaycastHit2D hit)
    {
        // RaycastHit2D ����� �������� ���� Ÿ���� �����ߴ��� Ȯ��
        return hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer));
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // Ÿ���� ��Ȯ�� �������� ����� �ൿ�� ����
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // ���� �߿��� �̵� ����
        IsAttacking = true;
    }
}
