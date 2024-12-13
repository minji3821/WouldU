using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs; // �༺ ������ �迭

    [SerializeField]
    private PathManager pathManager;    // PathManager���� ��� ����

    private float objectSpeed;          // �̵� �ӵ�
    private Queue<GameObject> spawnQueue = new Queue<GameObject>(); // ���� ��⿭

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
        PrepareInitialSpawnQueue(); // �ʱ� ��⿭ �غ�
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

        // ��⿭���� ������Ʈ ��������
        GameObject selectedPrefab = spawnQueue.Dequeue();

        // �༺ ����
        GameObject spawnedObject = Instantiate(selectedPrefab);

        // DOTween ��� ��������
        Vector3[] randomPath = pathManager.GetRandomPath();

        // ������Ʈ �ʱ� ��ġ�� ����� ���������� ����
        spawnedObject.transform.position = randomPath[0];

        // �̵� ����
        ObjectMove mover = spawnedObject.GetComponent<ObjectMove>();
        if (mover != null)
        {
            mover.MoveAlongPath(randomPath, objectSpeed);
        }

        // ���ο� ���� ������Ʈ�� ��⿭�� �߰��Ͽ� �׻� 3�� ����
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
        // ��⿭�� ��������Ʈ ������ UIManager�� ����
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
