using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject[] backgroundImages; 
    public GameObject playerCharacter; 
    public CanvasGroup canvasGroup;


    public IEnumerator StartGameScene()
    {
        yield return StartCoroutine(SetDeactiveCanvasUI());
        yield return StartCoroutine(ScatterBackgrounds());
        yield return StartCoroutine(MovePlayerCharacter());

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator SetDeactiveCanvasUI()
    {
        float duration = 2f; // ���̵� �ƿ� �ð�
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration); 
            canvasGroup.alpha = alpha; 

            yield return null;
        }
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

    }


    private IEnumerator ScatterBackgrounds()
    {
        float duration = 2f; // ������� �ð�
        float elapsed = 0f;

        // �� ��� �̹����� ���� ����� ó��
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                foreach (GameObject backgroundImage in backgroundImages)
                {
                    if (backgroundImage != null)
                    {
                        SpriteRenderer backgroundSprite = backgroundImage.GetComponent<SpriteRenderer>();
                        if (backgroundSprite != null)
                        {
                            Color originalColor = backgroundSprite.color;
                            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration); // ������ ���İ� ��ȭ
                            backgroundSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                        }
                    }
                }

                yield return null;
            }
        }

        // ���İ��� 0 �̵Ǹ�  ��Ȱ��ȭ
        foreach (GameObject backgroundImage in backgroundImages)
        {
            if (backgroundImage != null)
            {
                backgroundImage.SetActive(false);
            }
        }
    }

    private IEnumerator MovePlayerCharacter()
    {
        if (playerCharacter == null) yield break; // �÷��̾� ĳ���Ͱ� ������ �ߴ�

        float duration = 2f;
        float elapsed = 0f;

        Vector3 startPos = playerCharacter.transform.position;
        Vector3 endPos = startPos + new Vector3(0f, 5f, 0f); // Y������ 5��ŭ �̵�

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            playerCharacter.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }
    }
}
