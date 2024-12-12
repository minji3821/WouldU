using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    public bool HasCollided { get; private set; } = false; // �浹 ���� �÷���

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ����� �߷� ��ġ �Ǵ� �ٸ� �༺�� ���� �÷��� Ȱ��ȭ
        if (collision.gameObject.CompareTag("Gravity") || collision.gameObject.CompareTag("Obj_01") ||
            collision.gameObject.CompareTag("Obj_02") || collision.gameObject.CompareTag("Obj_03") ||
            collision.gameObject.CompareTag("Obj_04") || collision.gameObject.CompareTag("Obj_05") ||
            collision.gameObject.CompareTag("Obj_06"))
        {
            HasCollided = true;
        }
    }
}
