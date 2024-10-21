using System.Collections;
using UnityEngine;
using UnityEngine.UI; // UI�� �����Ϸ��� UnityEngine.UI ���ӽ����̽� �ʿ�

public class BuffAnim : MonoBehaviour
{
    public Image buffIconImage; // ���� ������ UI �̹���
    private float time = 0;
    private float blinkTime = 0.1f;
    private float xtime = 0;
    private float waitTime = 0.2f;

    // ���� ���� �ð� ����
    private float buffDuration = 7f;

    private void Awake()
    {
        if (buffIconImage == null)
        {
            buffIconImage = GetComponent<Image>(); // Image ������Ʈ�� ������
        }
    }

    public void StartBuff(float duration)
    {
        buffDuration = duration;
        time = 0f;
        xtime = 0f;
        waitTime = 0.2f;
        buffIconImage.color = new Color(1, 1, 1, 1); // ó���� �������� ������ ���̵��� ����
        buffIconImage.gameObject.SetActive(true); // ���� �������� Ȱ��ȭ
    }

    private void Update()
    {
        if (time < buffDuration - 3f) // ������ ������ ������ �������� ����
        {
            buffIconImage.color = new Color(1, 1, 1, 1); // �������� ������ ����
        }
        else // ������ 3�� ���� �����̰� ó��
        {
            if (xtime < blinkTime)
            {
                buffIconImage.color = new Color(1, 1, 1, 1 - xtime * 10); // �����Ÿ���
            }
            else if (xtime < waitTime + blinkTime)
            {
                // ��� �ð� ���� �ƹ� ��ȭ ����
            }
            else
            {
                buffIconImage.color = new Color(1, 1, 1, (xtime - (waitTime + blinkTime)) * 10); // �ٽ� ����
                if (xtime > waitTime + blinkTime * 2)
                {
                    xtime = 0;
                    waitTime *= 0.8f; // �����̴� �ӵ� ����
                    if (waitTime < 0.02f)
                    {
                        StopBuff(); // ������ ������ �������� ��Ȱ��ȭ
                    }
                }
            }
            xtime += Time.deltaTime;
        }
        time += Time.deltaTime;
    }

    public void StopBuff()
    {
        buffIconImage.gameObject.SetActive(false); // ���� �������� ��Ȱ��ȭ
        time = 0f;
        xtime = 0f;
        waitTime = 0.2f; // �ʱ�ȭ
    }
}
