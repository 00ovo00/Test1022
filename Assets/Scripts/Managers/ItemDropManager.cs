using UnityEngine;
using static UnityEditor.Progress;

public class ItemDropManager : MonoBehaviour
{
    public GameObject itemPrefab; // ����� ������ ������
    public ItemSO[] droppableItems; // ��� ������ ������ ���
    public float dropChance = 1f; // ������ ��� Ȯ��

    public void DropItem(Vector3 dropPosition)
    {
        if (Random.value <= dropChance) // ��� ���� ���⼭ ����
        {
            Debug.Log("�������� ���������ϴ�.");
            // ���� ������ ����
            int randomIndex = Random.Range(0, droppableItems.Length);
            ItemSO droppedItem = droppableItems[randomIndex];

            // ������ ����
            GameObject itemObject = Instantiate(itemPrefab, dropPosition, Quaternion.identity);
            // ������ ����
            ItemFeatures itemFeatures = itemObject.GetComponent<ItemFeatures>();
            itemFeatures.SetItem(droppedItem); 
        }
    }
}
