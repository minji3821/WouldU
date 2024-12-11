using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs; // 행성 프리팹 배열

    [SerializeField]
    private PathManager pathManager;    // PathManager에서 경로 참조

    private float objectSpeed;          // 이동 속도
    private Queue<GameObject> spawnQueue = new Queue<GameObject>(); // 스폰 대기열

    public void UpdateSettings(int objectCount, float speed)
    {
        objectSpeed = speed;
        PrepareInitialSpawnQueue(); // 초기 대기열 준비
    }

    public void SpawnObject()
    {
        if (spawnQueue.Count == 0)
        {
            Debug.LogError("Spawn Queue is empty! Something went wrong.");
            return;
        }

        // 대기열에서 오브젝트 가져오기
        GameObject selectedPrefab = spawnQueue.Dequeue();

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

        // 새로운 랜덤 오브젝트를 대기열에 추가하여 항상 3개 유지
        EnqueueRandomObject();
    }

    private void PrepareInitialSpawnQueue()
    {
        spawnQueue.Clear();
        for (int i = 0; i < 3; i++)
        {
            EnqueueRandomObject();
        }
    }

    private void EnqueueRandomObject()
    {
        GameObject randomPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
        spawnQueue.Enqueue(randomPrefab);
    }
}
