using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    public bool HasCollided { get; private set; } = false; // 충돌 여부 플래그

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 대상이 중력 장치 또는 다른 행성일 때만 플래그 활성화
        if (collision.gameObject.CompareTag("Gravity") || collision.gameObject.CompareTag("Obj_01") ||
            collision.gameObject.CompareTag("Obj_02") || collision.gameObject.CompareTag("Obj_03") ||
            collision.gameObject.CompareTag("Obj_04") || collision.gameObject.CompareTag("Obj_05") ||
            collision.gameObject.CompareTag("Obj_06"))
        {
            HasCollided = true;
        }
    }
}
