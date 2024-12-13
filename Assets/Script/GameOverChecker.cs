using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField]
    private Transform gravityCenter; // 중력 장치의 Transform
    [SerializeField]
    private string[] planetTags = { "Obj_01", "Obj_02", "Obj_03", "Obj_04", "Obj_05", "Obj_06" }; // 행성 태그 리스트

    private LineRenderer lineRenderer; // CircularBoundary에서 사용한 LineRenderer
    private float boundaryRadius; // 원형 선의 반지름
    private bool isAnyPlanetTouching = false; // 행성이 선에 닿아있는지 상태를 저장
    
    private void Start()
    {
        GameObject obj;
        obj = GameObject.Find("UIManager");
        if (!obj.TryGetComponent<LineRenderer>(out lineRenderer))
            Debug.Log("GameOverChecker.cs - lineRenderer 참조 실패");

        boundaryRadius = Camera.main.orthographicSize * 0.9f;
    }

    private void Update()
    {
        if (GameManager.Inst.isGaming)
            CheckPlanetPositions();
    }

    private void CheckPlanetPositions()
    {
        isAnyPlanetTouching = false;

        // 각 태그별로 행성 오브젝트 검색
        foreach (string tag in planetTags)
        {
            GameObject[] planets = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject planet in planets)
            {
                ObjectState objectState = planet.GetComponent<ObjectState>();
                if (objectState == null || !objectState.HasCollided) continue;

                float planetRadius = planet.GetComponent<Collider2D>().bounds.extents.x; // 행성의 반지름 계산
                float distance = Vector3.Distance(gravityCenter.position, planet.transform.position);
                float distanceToBoundary = distance + planetRadius; // 행성 경계까지의 거리 계산
                if (distance > boundaryRadius)
                {
                    // 행성의 중심점이 선을 넘는 경우 -> 게임 오버
                    GameManager.Inst.GameOver();
                    Debug.Log("hihi");
                    return;
                }
                else if (distanceToBoundary >= boundaryRadius && distance <= boundaryRadius)
                {
                    // 행성이 선에 닿아있는 경우
                    isAnyPlanetTouching = true;
                }
            }
        }

        UpdateLineColor();
    }

    private void UpdateLineColor()  // 선 색상 업데이트
    {
        if (isAnyPlanetTouching)
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
        else
        {
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
    }
}
