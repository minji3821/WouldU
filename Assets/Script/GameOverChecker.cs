using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField]
    private Transform gravityCenter; // �߷� ��ġ�� Transform
    [SerializeField]
    private string[] planetTags = { "Obj_01", "Obj_02", "Obj_03", "Obj_04", "Obj_05", "Obj_06" }; // �༺ �±� ����Ʈ

    private LineRenderer lineRenderer; // CircularBoundary���� ����� LineRenderer
    private float boundaryRadius; // ���� ���� ������
    private bool isAnyPlanetTouching = false; // �༺�� ���� ����ִ��� ���¸� ����
    
    private void Start()
    {
        GameObject obj;
        obj = GameObject.Find("UIManager");
        if (!obj.TryGetComponent<LineRenderer>(out lineRenderer))
            Debug.Log("GameOverChecker.cs - lineRenderer ���� ����");

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

        // �� �±׺��� �༺ ������Ʈ �˻�
        foreach (string tag in planetTags)
        {
            GameObject[] planets = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject planet in planets)
            {
                ObjectState objectState = planet.GetComponent<ObjectState>();
                if (objectState == null || !objectState.HasCollided) continue;

                float planetRadius = planet.GetComponent<Collider2D>().bounds.extents.x; // �༺�� ������ ���
                float distance = Vector3.Distance(gravityCenter.position, planet.transform.position);
                float distanceToBoundary = distance + planetRadius; // �༺ �������� �Ÿ� ���
                if (distance > boundaryRadius)
                {
                    // �༺�� �߽����� ���� �Ѵ� ��� -> ���� ����
                    GameManager.Inst.GameOver();
                    Debug.Log("hihi");
                    return;
                }
                else if (distanceToBoundary >= boundaryRadius && distance <= boundaryRadius)
                {
                    // �༺�� ���� ����ִ� ���
                    isAnyPlanetTouching = true;
                }
            }
        }

        UpdateLineColor();
    }

    private void UpdateLineColor()  // �� ���� ������Ʈ
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
