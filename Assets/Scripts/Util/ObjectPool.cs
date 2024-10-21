using System.Collections.Generic;
using UnityEngine;

public partial class ObjectPool : MonoBehaviour
{
    // ������Ʈ Ǯ�� �����ϱ� ���� ����ȭ ���� Ŭ����
    [System.Serializable]
    public class Pool
    {
        public string tag;  
        public GameObject prefab; 
        public int size; 
    }

    public List<Pool> Pools;
    protected Dictionary<string, Queue<GameObject>> PoolDictionary;

    protected virtual void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>(); // ������Ʈ Ǯ�� ���� ��ųʸ� �ʱ�ȭ
        InitializePools(); // ���ǵ� ��Ͽ� ���� ������Ʈ Ǯ ����
    }

    protected void InitializePools() // ������Ʈ Ǯ�� �ʱ�ȭ�ϴ� �޼���
    {
        if (Pools.Count <= 0) return; // ���ǵ� Ǯ�� ������ �޼��� ����

        foreach (var pool in Pools)
        {
            CreatePool(pool.tag, pool.prefab, pool.size);
        }
    }

    public virtual void CreatePool(string tag, GameObject prefab, int size) // Ư�� ������Ʈ Ǯ�� �����ϴ� �޼���
    {
        Queue<GameObject> objectPool = new Queue<GameObject>(); // Ǯ�� ���� �� ť ����
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, transform); // �������� �ν��Ͻ�ȭ�ϰ� ��Ȱ��ȭ�� �� Ǯ ť�� �߰�
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
        PoolDictionary.Add(tag, objectPool);
    }

    public GameObject SpawnFromPool(string tag) // �±׸� ����� Ǯ���� ������Ʈ�� �����ϴ� �޼���
    {
        if (!PoolDictionary.ContainsKey(tag)) // Ǯ�� �����ϴ��� Ȯ��
            return null;

        GameObject obj = PoolDictionary[tag].Dequeue(); // Ǯ���� ��ü�� �����ϰ� ����� �� Ȱ��ȭ
        PoolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        return obj;
    }

    public GameObject SpawnFromPool(string tag, GameObject spawnPoint) // Ư�� ���� ����Ʈ���� ������Ʈ�� �����ϴ� �����ε� �޼���
    {
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = PoolDictionary[tag].Dequeue();
        PoolDictionary[tag].Enqueue(obj);
        obj.transform.position = spawnPoint.transform.position; 
        obj.SetActive(true);
        return obj;
    }
}