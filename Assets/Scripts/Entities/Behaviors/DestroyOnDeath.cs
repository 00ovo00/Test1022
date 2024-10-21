using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private ItemDropManager itemDropManager;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        // ���� ���� ��ü�� healthSystem��
        healthSystem.OnDeath += OnDeath;
        itemDropManager = FindObjectOfType<ItemDropManager>();
    }

    void OnDeath()
    {
        // �� ��ü ��Ȱ��ȭ
        gameObject.SetActive(false);
        healthSystem.CurrentHealth += healthSystem.MaxHealth;
        // ųī��Ʈ ����
        DataManager.Instance.IncrementKillCount();
        // ���� ��ġ���� ������ ���
        itemDropManager.DropItem(transform.position); 
    }
}