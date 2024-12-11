using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ObjectConnectionManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Transform> connectedObjects = new List<Transform>(); // 연결된 오브젝트들

    private void Awake()
    {
        // LineRenderer 초기화
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // 초기화

        // LineRenderer 설정
        lineRenderer.sortingLayerName = "Default"; // 적절한 레이어 이름 설정
        lineRenderer.sortingOrder = 10;           // 높은 값으로 설정하여 오브젝트 위로 표시
        lineRenderer.widthMultiplier = 5f;     // 선 두께 설정
        lineRenderer.startWidth = 0.1f;          // 시작 선 두께
        lineRenderer.endWidth = 0.1f;            // 끝 선 두께
        lineRenderer.useWorldSpace = true;        // 월드 좌표계에서 작동
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 선 기본 머티리얼
    }

    private void Update()
    {
        UpdateLines(); // 매 프레임마다 선 위치 업데이트
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 같은 태그를 가진 오브젝트와만 연결
        if (collision.gameObject.CompareTag(this.tag))
        {
            Transform otherObject = collision.transform;
            if (!connectedObjects.Contains(otherObject))
            {
                connectedObjects.Add(otherObject); // 연결된 오브젝트 목록에 추가
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 연결이 해제된 경우 목록에서 제거
        if (collision.gameObject.CompareTag(this.tag))
        {
            Transform otherObject = collision.transform;
            if (connectedObjects.Contains(otherObject))
            {
                connectedObjects.Remove(otherObject); // 연결된 오브젝트 목록에서 제거
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
}
