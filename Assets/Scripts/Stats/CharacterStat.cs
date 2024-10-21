using System;
using UnityEngine;

// Add �����ϰ�, Multiple�ϰ�, �������� Override�ϴ� ����
// enum���� ���� ������ ���εǾ��ֱ� ������ ���� (0, 1, 2,...) 
// => ���Ŀ� ���� Ȱ���ϸ� ������������ ����Ȱ���ؼ� ü�������� ����ȿ�� ������� ���� ����
// Q: Override�� �������� ������ ���� ȿ���� �������?
// A: ���ݷ��� �����ؾ��ϴ� Ư�� �����̳� �⺻ ���ݷ� ���뿡 Ȱ�� ����
public enum StatsChangeType
{
    Add, // 0, ������ n��ŭ ����
    Multiple, // 1 , ������ n% ����
    Override, // 2 , ������ n���� ����
}

// Ŭ������ Serializable�� ����θ� �����Ǿ� ������ [Serializable]�� �ٿ� �����Ϳ��� Ȯ�� ����!
// [Serializable]
[System.Serializable]   // ������ ����ó�� ����� �� �ְ� ������ִ� Attribute
public class CharacterStat
{
    public StatsChangeType statsChangeType;
    // [Range(,)] �����̴�
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO;
}