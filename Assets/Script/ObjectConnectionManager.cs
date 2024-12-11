using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ObjectConnectionManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Transform> connectedObjects = new List<Transform>(); // ����� ������Ʈ��

    private void Awake()
    {
        // LineRenderer �ʱ�ȭ
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // �ʱ�ȭ

        // LineRenderer ����
        lineRenderer.sortingLayerName = "Default"; // ������ ���̾� �̸� ����
        lineRenderer.sortingOrder = 10;           // ���� ������ �����Ͽ� ������Ʈ ���� ǥ��
        lineRenderer.widthMultiplier = 5f;     // �� �β� ����
        lineRenderer.startWidth = 0.1f;          // ���� �� �β�
        lineRenderer.endWidth = 0.1f;            // �� �� �β�
        lineRenderer.useWorldSpace = true;        // ���� ��ǥ�迡�� �۵�
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �� �⺻ ��Ƽ����
    }

    private void Update()
    {
        UpdateLines(); // �� �����Ӹ��� �� ��ġ ������Ʈ
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� �±׸� ���� ������Ʈ�͸� ����
        if (collision.gameObject.CompareTag(this.tag))
        {
            Transform otherObject = collision.transform;
            if (!connectedObjects.Contains(otherObject))
            {
                connectedObjects.Add(otherObject); // ����� ������Ʈ ��Ͽ� �߰�
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ������ ������ ��� ��Ͽ��� ����
        if (collision.gameObject.CompareTag(this.tag))
        {
            Transform otherObject = collision.transform;
            if (connectedObjects.Contains(otherObject))
            {
                connectedObjects.Remove(otherObject); // ����� ������Ʈ ��Ͽ��� ����
            }
        }
    }

    private void UpdateLines()
    {
        if (connectedObjects.Count >= 1) // �ּ� �ϳ� �̻��� ������ ���� ���
        {
            // LineRenderer�� ��ġ ������Ʈ
            lineRenderer.positionCount = connectedObjects.Count * 2;

            int index = 0;

            // ����� ������Ʈ�� �߽��� ���� �� ����
            foreach (Transform connectedObject in connectedObjects)
            {
                lineRenderer.SetPosition(index++, transform.position);          // ������
                lineRenderer.SetPosition(index++, connectedObject.position);   // ����
            }
        }
        else
        {
            // ����� ������Ʈ�� ������ �� ����
            lineRenderer.positionCount = 0;
        }
    }
}
