using System.Runtime.CompilerServices;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;   // ���� ��ü

    [SerializeField] private Transform projectileSpawnPosition; // ȭ�� ���� ����
    private Vector2 aimDirection = Vector2.right;   // ���� ����
    private PlayerInputController playerInputController;
    private void Awake()
    {
        playerInputController= GetComponent<PlayerInputController>();
        controller = GetComponent<TopDownController>();
    }

    void Start()
    {
        controller.OnAttackEvent += OnShoot;

        /* ���� OnLookEvent�� ��ϵ� �̺�Ʈ */
        // 1. TopDownAimRotation.OnAim(Vec2)
        // 2. TopDownShooting.OnAim(Vec2)
        // �ϳ��� ��������Ʈ�� ���� �� �Լ� ���(multicast delegate)
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        // ȭ���� ���ư��� ���� ����
        aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSO attackSO)
    {
        // ���Ÿ� ���ݸ� �ٷ�
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if (rangedAttackSO == null) return;

        float projectilesAngleSpace = rangedAttackSO.multipleProjectilesAngel;  // �߻簢
        int numberOfProjectilesPerShot = rangedAttackSO.numberofProjectilesPerShot; // �ѹ��� ������ ȭ�� ����

        // �߰����� �������°� �ƴ϶� minangle���� Ŀ���鼭 ��� ������ ���� 
        // �Ʒ����� ���������� ���������� Ŀ��
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackSO.multipleProjectilesAngel;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            // �������� ���ϴ� randomSpread�� �߰�
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        Debug.Log(rangedAttackSO);
        // ������Ʈ Ǯ�� Ȱ���� ����
        GameObject obj = SpawnManager.Instance.objectPool.SpawnFromPool(rangedAttackSO.bulletNameTag);
       
        // �߻�ü �⺻ ����
        Debug.Log("��� ��ġ"+ projectileSpawnPosition.position);
        obj.transform.position = projectileSpawnPosition.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);

        if (rangedAttackSO.bulletNameTag == "Arrow")
        {
            playerInputController.CallAttack();
        }
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // ���� ȸ���ϱ� : ���ʹϾ� * ���� ��
        return Quaternion.Euler(0, 0, degree) * v;
    }
}