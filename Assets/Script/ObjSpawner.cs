using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs; // �༺ ������ �迭

    [SerializeField]
    private PathManager pathManager;    // PathManager���� ��� ����

    private float objectSpeed;    // �̵� �ӵ�

    public void UpdateSettings(int objectCount, float speed)
    {
        objectSpeed = speed;
    }

    public void SpawnObject()
    {
        // ���� �༺ ������ ����
        GameObject selectedPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

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

    }
}



