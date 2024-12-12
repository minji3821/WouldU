using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBoundary : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        // LineRenderer 설정
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.loop = true;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 기본 셰이더
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        lineRenderer.sortingLayerName = "Default"; // 원하는 Sorting Layer 이름
        lineRenderer.sortingOrder = -1; // 행성보다 뒤에 렌더링되도록 설정

        // 디바이스 화면 크기에 맞춰 원 반지름 계산
        float radius = CalculateRadius();

        // 원형 선 그리기
        DrawCircle(radius);
    }

    private float CalculateRadius()
    {
        return Camera.main.orthographicSize * 0.9f; // 90% 크기 (테두리 여유)
    }

    private void DrawCircle(float radius)
    {
        int segments = 100; // 원의 세부 정도
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
