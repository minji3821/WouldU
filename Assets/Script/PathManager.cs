using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Transform gravityTarget; // �߷���ġ Transform

    // �� ����� ���� ���� (6���� ���)
    public Vector3[][] paths = new Vector3[6][];

    private void Awake()
    {
        Vector3 targetPosition = gravityTarget.position;

        paths[0] = new Vector3[]
        {
            new Vector3(-11, 3, 0), // ������
            new Vector3(-5, 2, 0),  // ������ (�)
            targetPosition          // ������
        };

        paths[1] = new Vector3[]
        {
            new Vector3(-11, 0, 0),
            new Vector3(-5, 0.5f, 0),
            targetPosition
        };

        paths[2] = new Vector3[]
        {
            new Vector3(-7, -6, 0),
            new Vector3(-5, -2, 0),
            targetPosition
        };

        // ��� 4: ���� �ϴܿ��� �߷���ġ��
        paths[3] = new Vector3[]
        {
            new Vector3(11, 3, 0),
            new Vector3(5, 2, 0),
            targetPosition
        };

        // ��� 5: �߾� �ϴܿ��� �߷���ġ��
        paths[4] = new Vector3[]
        {
            new Vector3(11, 0, 0),
            new Vector3(5, 0.5f, 0),
            targetPosition
        };

        // ��� 6: ���� �ϴܿ��� �߷���ġ��
        paths[5] = new Vector3[]
        {
            new Vector3(7, -6, 0),
            new Vector3(5, -2, 0),
            targetPosition
        };

    }

        

    public Vector3[] GetRandomPath()
    {
        return paths[Random.Range(0, paths.Length)];
    }
}
