using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player { get; private set; }
    
    [SerializeField] private string playerTag = "Player";

    private bool isGameOver = false;

    public static event Action OnTitle;
    public static event Action OnGameStart;
    public HealthSystem playerHealthSystem;

    private void Awake()
    {
        // �ϳ��� �����ǵ��� ����
        if (Instance != null)
        {
            Destroy(gameObject);
            return;

        }
            
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag);
        Debug.Log(Player.gameObject);
        
    }
    private void OnEnable()
    {
        // �� ��ε帶�� ȣ��
        SceneManager.sceneLoaded += FindGameScene;
    }
    private void Start()
    {
   

    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0.0f;
        Debug.Log("GameOver");
    }
    public void ReLoadGame()
    {
        Time.timeScale = 1.0f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� ��ε�
    }
    public void FindGameScene(Scene scene, LoadSceneMode mode)
    {
        // �� ��ε� �ÿ� �� ������ ���� �ٸ� �̺�Ʈ ȣ��
        switch (scene.name)
        {
            case "TitleScene":
                OnTitle?.Invoke();
                Debug.Log("OnTitle");
                break;
            case "MainScene":
                OnGameStart?.Invoke();
                Debug.Log("OnStart");
                // ���� ���� ��� ������� �÷��̾�� ���ε�
                Player = GameObject.FindGameObjectWithTag(playerTag);
                break;

        }
    }
    public void StartGame()
    {

        string sceneName = "MainScene";
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            if (scene.name == "MainScene")
            {
                OnGameStart?.Invoke();
                Player = GameObject.FindGameObjectWithTag(playerTag);

                SceneManager.sceneLoaded -= FindGameScene;
            }
        };
    }

   
}