using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs; // 행성 프리팹 배열

    [SerializeField]
    private PathManager pathManager;    // PathManager에서 경로 참조

    private float objectSpeed;    // 이동 속도

    public void UpdateSettings(int objectCount, float speed)
    {
        objectSpeed = speed;
    }

    public void SpawnObject()
    {
        // 랜덤 행성 프리팹 선택
        GameObject selectedPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        // 행성 생성
        GameObject spawnedObject = Instantiate(selectedPrefab);

        // DOTween 경로 가져오기
        Vector3[] randomPath = pathManager.GetRandomPath();

        // 오브젝트 초기 위치를 경로의 시작점으로 설정
        spawnedObject.transform.position = randomPath[0];

        // 이동 설정
        ObjectMove mover = spawnedObject.GetComponent<ObjectMove>();
        if (mover != null)
        {
            mover.MoveAlongPath(randomPath, objectSpeed);
        }

    }
}



