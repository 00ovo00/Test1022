using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    // ���ݿ� ���� ���� �����͸� ����Ƽ ������ �󿡼� ���ϰ� ����
    // SO�� ��� ������ ��ΰ� �� SO�� �ٶ󺸰� �Ǿ� �ߺ� ������ ���ְ� �ϰ��� ����
    // ����� �з��Ͽ� ������ ���
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool isOnKnockback;
    public float knockbackPower;
    public float knockbackTime;
}