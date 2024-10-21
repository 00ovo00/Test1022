using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            HealthSystem playerHealthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
            if (playerHealthSystem != null)
                playerHealthSystem.OnGameOver += ShowGameOverScreen;
        }

        gameOverPanel.SetActive(false); // ���� �� ��Ȱ��ȭ
        restartButton.onClick.AddListener(RestartGame); // ����� ��ư Ŭ�� �� ���� ����� ó��
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);

        float currentScore = DataManager.Instance.GetScore();
        float bestScore = DataManager.Instance.GetBestScore();

        if (currentScore > bestScore)
        {
            // ������ �ְ��� �����ϵ��� �޼ҵ� ȣ��
            DataManager.Instance.SetBestScore(currentScore);
        }

        scoreText.text = currentScore.ToString();
        bestScoreText.text = DataManager.Instance.GetBestScore().ToString();
    }

    private void RestartGame()
    {
        GameManager.Instance.ReLoadGame();
    }
}