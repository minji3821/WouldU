using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMove : MonoBehaviour
{
    private Tweener moveTweener;

    public void MoveAlongPath(Vector3[] path, float speed)
    {
        // DOTween으로 경로 이동
        moveTweener = transform.DOPath(path, speed, PathType.CatmullRom).SetEase(Ease.Linear);
    }

    private void OnCollisionEnter2D(Collision2D other) // 정확한 메서드 이름으로 수정
    {
        // 중력 장치 또는 이미 고정된 오브젝트와 충돌 시
        if (other.transform.CompareTag("Gravity") || other.transform.parent?.CompareTag("Gravity") == true)
        {
            // 이동 정지
            if (moveTweener != null && moveTweener.IsPlaying())
            {
                moveTweener.Kill();
                // GameManager에 알림
                FindObjectOfType<GameManager>().OnObjectFixed();
            }
            Transform gravityParent = other.transform.root.CompareTag("Gravity")? other.transform.root : null;
            // 중력 장치를 부모로 설정
            transform.SetParent(gravityParent);

            
        }
    }
}
