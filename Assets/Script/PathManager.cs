using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Transform gravityTarget; // 중력장치 Transform

    // 각 경로의 점을 정의 (6개의 경로)
    public Vector3[][] paths = new Vector3[6][];

    private void Awake()
    {
        Vector3 targetPosition = gravityTarget.position;

        paths[0] = new Vector3[]
        {
            new Vector3(-11, 3, 0), // 시작점
            new Vector3(-5, 2, 0),  // 제어점 (곡선)
            targetPosition          // 도착점
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

        // 경로 4: 좌측 하단에서 중력장치로
        paths[3] = new Vector3[]
        {
            new Vector3(11, 3, 0),
            new Vector3(5, 2, 0),
            targetPosition
        };

        // 경로 5: 중앙 하단에서 중력장치로
        paths[4] = new Vector3[]
        {
            new Vector3(11, 0, 0),
            new Vector3(5, 0.5f, 0),
            targetPosition
        };

        // 경로 6: 우측 하단에서 중력장치로
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
