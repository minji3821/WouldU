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

    [SerializeField] private PreviewUIManager previewUIManager;

    private void Start()
    {
        GameManager.Inst.isGaming = true;
        float currentSpeed = GameManager.Inst.objectSpeedsPerLevel[GameManager.Inst.currentLevel - 1];
        UpdateSettings(3, currentSpeed);
        SpawnObject();
    }

    public void UpdateSettings(int objectCount, float speed)
    {
        objectSpeed = speed;
        PrepareInitialSpawnQueue(); // 초기 대기열 준비
    }

    public void SpawnObject()
    {
        Debug.Log("Spawn");
        Debug.Log(spawnQueue.Count);
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
        UpdatePreviewUI();
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
        GameObject randomPrefab = objectPrefabs[Random.Range(0, GameManager.Inst.currentObjectCount + 1)];
        spawnQueue.Enqueue(randomPrefab);
    }

    private void UpdatePreviewUI()
    {
        // 대기열의 스프라이트 정보를 UIManager로 전달
        List<Sprite> sprites = new List<Sprite>();
        foreach (var prefab in spawnQueue)
        {
            Sprite sprite = prefab.GetComponent<SpriteRenderer>().sprite;
            if (sprite != null)
            {
                sprites.Add(sprite);
            }
        }

        previewUIManager.UpdatePreview(sprites);
    }
}
