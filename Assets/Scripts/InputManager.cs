using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Rigidbody2D r2bd;

    [SerializeField]
    private float speed;

    void Start()
    {
        // Rigidbody2D ĳ��
        r2bd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Vertical��� Horizontal���� �Է��� �޾ƿ���
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        // ����ȭ
        // vertical, horizontal ��� 1�� ���, direction�� ũ��� 1���� ũ�� �� �� �ִµ�(��Ʈ2), �̸� 1�� ������.
        Vector2 direction = new Vector2(horizontal, vertical);
        direction = direction.normalized;

        // rigidbody.velocity�� �ش� ��ü�� 1�ʴ� �����̴� �Ÿ�
        r2bd.velocity = direction * speed;
    }
}
