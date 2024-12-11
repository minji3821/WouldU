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

    public void UpdateSettings(int objectCount, float speed)
    {
        objectSpeed = speed;
        PrepareInitialSpawnQueue(); // �ʱ� ��⿭ �غ�
    }

    public void SpawnObject()
    {
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
