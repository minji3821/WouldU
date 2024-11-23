using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public Transform gravityCenter; // 중력장치 Transform (화면 중앙 고정)
    public float gravityForce = 5f; // 중력 세기

    private void FixedUpdate()
    {
        // 중력장치의 자식 오브젝트들을 순회
        foreach (Transform child in gravityCenter)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 중력장치를 향한 방향 계산
                Vector2 direction = (gravityCenter.position - child.position).normalized;

                // 중력장치 방향으로 힘을 가함
                rb.AddForce(direction * gravityForce);
            }
        }
    }
}
