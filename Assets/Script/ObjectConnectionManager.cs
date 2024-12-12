using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ObjectConnectionManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Transform> connectedObjects = new List<Transform>(); // ���� ����� ������Ʈ��
    private HashSet<Transform> visitedObjects = new HashSet<Transform>(); // ���� ���� Ž�� �� �湮�� ������Ʈ

    private void Awake()
    {
        // LineRenderer �ʱ�ȭ
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // �ʱ�ȭ

        // LineRenderer ����
        lineRenderer.sortingLayerName = "Default"; // ������ ���̾� �̸� ����
        lineRenderer.sortingOrder = 1;           // ���� ������ �����Ͽ� ������Ʈ ���� ǥ��
        lineRenderer.widthMultiplier = 1f;       // �� �β� ����
        lineRenderer.startWidth = 0.05f;         // ���� �� �β�
        lineRenderer.endWidth = 0.05f;           // �� �� �β�
        lineRenderer.useWorldSpace = true;       // ���� ��ǥ�迡�� �۵�
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �� �⺻ ��Ƽ����
    }

    private void Update()
    {
        UpdateLines(); // �� �����Ӹ��� �� ��ġ ������Ʈ
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(this.tag))
        {
            Transform otherObject = collision.transform;

            // �ߺ� �߰� ����
            if (!connectedObjects.Contains(otherObject))
            {
                connectedObjects.Add(otherObject);
                CheckAndDeleteConnectedObjects();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(this.tag))
        {
            Transform otherObject = collision.transform;

            // ���� ���� Ȯ�� �� ����
            if (connectedObjects.Contains(otherObject))
            {
                connectedObjects.Remove(otherObject);
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

    private void CheckAndDeleteConnectedObjects()
    {
        // �湮 �ʱ�ȭ �� ���� ���� Ž��
        visitedObjects.Clear();
        int totalConnectedCount = GetConnectedCount(transform);


        // 4�� �̻� ����Ǿ��ٸ� ����
        if (totalConnectedCount >= 4)
        {
            DeleteConnectedObjects();
        }
    }

    private int GetConnectedCount(Transform current)
    {
        if (visitedObjects.Contains(current))
        {
            return 0; // �̹� �湮�� ������Ʈ�� �ǳʶٱ�
        }

        visitedObjects.Add(current);
        int count = 1; // ���� ������Ʈ ����

        // ���� ������Ʈ�� ����� ������Ʈ�� Ž��
        ObjectConnectionManager connectionManager = current.GetComponent<ObjectConnectionManager>();
        if (connectionManager != null)
        {
            foreach (Transform connectedObject in connectionManager.connectedObjects)
            {
                count += GetConnectedCount(connectedObject); // ��������� Ž��
            }
        }

        return count;
    }

    private void DeleteConnectedObjects()
    {
        // �湮�� ��� ������Ʈ ����
        foreach (Transform obj in visitedObjects)
        {
            Destroy(obj.gameObject);
        }

    }
}