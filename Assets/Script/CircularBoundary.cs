using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBoundary : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        // LineRenderer ����
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.loop = true;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �⺻ ���̴�
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        lineRenderer.sortingLayerName = "Default"; // ���ϴ� Sorting Layer �̸�
        lineRenderer.sortingOrder = -1; // �༺���� �ڿ� �������ǵ��� ����

        // ����̽� ȭ�� ũ�⿡ ���� �� ������ ���
        float radius = CalculateRadius();

        // ���� �� �׸���
        DrawCircle(radius);
    }

    private float CalculateRadius()
    {
        return Camera.main.orthographicSize * 0.9f; // 90% ũ�� (�׵θ� ����)
    }

    private void DrawCircle(float radius)
    {
        int segments = 100; // ���� ���� ����
        Vector3[] points = new Vector3[segments + 1];
        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            points[i] = new Vector3(x, y, 0f);
            angle += 2 * Mathf.PI / segments;
        }

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
