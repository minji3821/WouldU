using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public Transform gravityCenter; // �߷���ġ Transform (ȭ�� �߾� ����)
    public float gravityForce = 5f; // �߷� ����

    private void FixedUpdate()
    {
        // �߷���ġ�� �ڽ� ������Ʈ���� ��ȸ
        foreach (Transform child in gravityCenter)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // �߷���ġ�� ���� ���� ���
                Vector2 direction = (gravityCenter.position - child.position).normalized;

                // �߷���ġ �������� ���� ����
                rb.AddForce(direction * gravityForce);
            }
        }
    }
}
