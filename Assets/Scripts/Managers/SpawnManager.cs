using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private GameObject[] spawnPointArray; // ���� ����Ʈ �迭
    public ObjectPool objectPool;
    private MonsterObjectPool monsterObjectPool;
    [SerializeField] private float spawnTime = 4;
    private List<string> poolNameList; // Ǯ �̸� ����Ʈ
    private float lastSpawnTime = 0f;

    public static SpawnManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }
    [System.Serializable]
    public class SpawnInfo    // ���� ���� ���� ���� Ŭ����

    {
        public int killCount;   // ������ ���� ų ī��Ʈ ����
        public string tag;     // ���� �±�
        public GameObject prefab;  // ���� ������
        public int size;       // ������ ������ ��
    }

    public List<SpawnInfo> spawnInpos; // ���� ���� ����Ʈ

    private void Start()
    {

        objectPool = GetComponent<ObjectPool>();
        monsterObjectPool = GetComponent<MonsterObjectPool>();
        spawnPointArray = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // óġ �� ��ȭ�� ���� ���� �ڵ鷯 ���
        DataManager.Instance.OnKillCountChanged += SpawnHandlerByKillCount;
        UpdateArray(); // Ǯ �̸� ����Ʈ ������Ʈ
    }

    private void Update()
    {
        SpawnTimeChecker(); // ���� �ð� üũ
    }

    public void SpawnTimeChecker()
    {
        lastSpawnTime += Time.deltaTime;

        if (lastSpawnTime < spawnTime) return; // ���� �ֱ� ������ �� ����

        Spawn();    // ���� ����

        lastSpawnTime = 0f; // ������ ���� �ð� �ʱ�ȭ

    }

    public void Spawn()
    {
        string currentSelectedPool = SelectRandomPool(); // ���� Ǯ ����
        Debug.Log("���õ� Ǯ" + currentSelectedPool);
        int randomIndex = Random.Range(0, spawnPointArray.Length); // ���� ���� ����Ʈ ����

        monsterObjectPool.SpawnFromPool(currentSelectedPool, spawnPointArray[randomIndex]); // ���� ���� , ����Ǯ, �������� Ȱ��

    }

    private void SpawnHandlerByKillCount(int killCount) // óġ ���� ���� ���� �ڵ鷯
    {
        foreach (var info in spawnInpos)
        {
            if (killCount == info.killCount && !poolNameList.Contains(info.tag)) // óġ ���� ��ġ�ϴ� ���� ������ ������ Ǯ �߰�

            {
                AddPool(info.tag, info.prefab, info.size);
                break;
            }
        }
    }

    public string SelectRandomPool()   // ���� Ǯ ���� �޼���

    {
        if (poolNameList == null || poolNameList.Count == 0) return null;  // Ǯ �̸� ����Ʈ�� ������ ����

        int randomIndex = Random.Range(0, poolNameList.Count);  // ���� �ε��� ����
        Debug.Log("����Ǯ" + randomIndex);
        return poolNameList[randomIndex];  // ���õ� Ǯ ��ȯ

    }

    private void AddPool(string tag, GameObject prefab, int size)   // ���ο� Ǯ �߰� �޼���
    {
        monsterObjectPool.CreatePool(tag, prefab, size);  // ���� Ǯ ����
        UpdateArray(); // Ǯ �̸� ����Ʈ ������Ʈ
    }

    private void UpdateArray()
    {
        poolNameList = monsterObjectPool.GetPoolNameList(); // ���� �̸� ����Ʈ �������� from MonsterObjectPool

    }
}
