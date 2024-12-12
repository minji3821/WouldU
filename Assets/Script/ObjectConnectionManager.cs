using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ObjectConnectionManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Transform> connectedObjects = new List<Transform>(); // 직접 연결된 오브젝트들
    private HashSet<Transform> visitedObjects = new HashSet<Transform>(); // 간접 연결 탐색 시 방문한 오브젝트

    private void Awake()
    {
        // LineRenderer 초기화
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // 초기화

        // LineRenderer 설정
        lineRenderer.sortingLayerName = "Default"; // 적절한 레이어 이름 설정
        lineRenderer.sortingOrder = 1;           // 높은 값으로 설정하여 오브젝트 위로 표시
        lineRenderer.widthMultiplier = 1f;       // 선 두께 설정
        lineRenderer.startWidth = 0.05f;         // 시작 선 두께
        lineRenderer.endWidth = 0.05f;           // 끝 선 두께
        lineRenderer.useWorldSpace = true;       // 월드 좌표계에서 작동
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 선 기본 머티리얼
    }

    private void Update()
    {
        UpdateLines(); // 매 프레임마다 선 위치 업데이트
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(this.tag))
        {
            Transform otherObject = collision.transform;

            // 중복 추가 방지
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

            // 존재 여부 확인 후 제거
            if (connectedObjects.Contains(otherObject))
            {
                connectedObjects.Remove(otherObject);
            }
        }
    }

    private void UpdateLines()
    {
        if (connectedObjects.Count >= 1) // 최소 하나 이상의 연결이 있을 경우
        {
            // LineRenderer에 위치 업데이트
            lineRenderer.positionCount = connectedObjects.Count * 2;

            int index = 0;

            // 연결된 오브젝트의 중심을 따라 선 생성
            foreach (Transform connectedObject in connectedObjects)
            {
                lineRenderer.SetPosition(index++, transform.position);          // 시작점
                lineRenderer.SetPosition(index++, connectedObject.position);   // 끝점
            }
        }
        else
        {
            // 연결된 오브젝트가 없으면 선 제거
            lineRenderer.positionCount = 0;
        }
    }

    private void CheckAndDeleteConnectedObjects()
    {
        // 방문 초기화 및 연결 상태 탐색
        visitedObjects.Clear();
        int totalConnectedCount = GetConnectedCount(transform);


        // 4개 이상 연결되었다면 삭제
        if (totalConnectedCount >= 4)
        {
            DeleteConnectedObjects();
        }
    }

    private int GetConnectedCount(Transform current)
    {
        if (visitedObjects.Contains(current))
        {
            return 0; // 이미 방문한 오브젝트는 건너뛰기
        }

        visitedObjects.Add(current);
        int count = 1; // 현재 오브젝트 포함

        // 현재 오브젝트의 연결된 오브젝트들 탐색
        ObjectConnectionManager connectionManager = current.GetComponent<ObjectConnectionManager>();
        if (connectionManager != null)
        {
            foreach (Transform connectedObject in connectionManager.connectedObjects)
            {
                count += GetConnectedCount(connectedObject); // 재귀적으로 탐색
            }
        }

        return count;
    }

    private void DeleteConnectedObjects()
    {
        // 방문한 모든 오브젝트 삭제
        foreach (Transform obj in visitedObjects)
        {
            Destroy(obj.gameObject);
        }

    }
}