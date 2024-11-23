using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMove : MonoBehaviour
{
    private Tweener moveTweener;

    public void MoveAlongPath(Vector3[] path, float speed)
    {
        // DOTween���� ��� �̵�
        moveTweener = transform.DOPath(path, speed, PathType.CatmullRom).SetEase(Ease.Linear);
    }

    private void OnCollisionEnter2D(Collision2D other) // ��Ȯ�� �޼��� �̸����� ����
    {
        // �߷� ��ġ �Ǵ� �̹� ������ ������Ʈ�� �浹 ��
        if (other.transform.CompareTag("Gravity") || other.transform.parent?.CompareTag("Gravity") == true)
        {
            // �̵� ����
            if (moveTweener != null && moveTweener.IsPlaying())
            {
                moveTweener.Kill();
                // GameManager�� �˸�
                FindObjectOfType<GameManager>().OnObjectFixed();
            }
            Transform gravityParent = other.transform.root.CompareTag("Gravity")? other.transform.root : null;
            // �߷� ��ġ�� �θ�� ����
            transform.SetParent(gravityParent);

            
        }
    }
}
