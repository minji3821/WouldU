using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private bool isDragging = false;
    private float rotationSpeed = 500f; // 회전 속도 조절용

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 터치 시작 시 위치 저장
            startTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentTouchPosition = Input.mousePosition;
            Vector2 direction = currentTouchPosition - startTouchPosition;

            if (direction.magnitude > 5f) // 드래그 최소 거리
            {
                // 현재 터치 위치가 화면의 오른쪽인지 확인
                bool isRightSide = currentTouchPosition.x > Screen.width / 2;
                float rotationAmount = rotationSpeed * Time.deltaTime;

                // 현재 드래그 방향에 따라 회전 방향 결정
                if (isRightSide)
                {
                    // 오른쪽 화면에서 드래그
                    if (direction.y < 0) // 아래로 드래그
                    {
                        transform.Rotate(Vector3.forward, -rotationAmount); // 시계 방향
                    }
                    else if (direction.y > 0) // 위로 드래그
                    {
                        transform.Rotate(Vector3.forward, rotationAmount); // 반시계 방향
                    }
                }
                else
                {
                    // 왼쪽 화면에서 드래그
                    if (direction.y < 0) // 아래로 드래그
                    {
                        transform.Rotate(Vector3.forward, rotationAmount); // 반시계 방향
                    }
                    else if (direction.y > 0) // 위로 드래그
                    {
                        transform.Rotate(Vector3.forward, -rotationAmount); // 시계 방향
                    }
                }

                // 시작 위치 업데이트
                startTouchPosition = currentTouchPosition; // 드래그를 따라 업데이트
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 터치 종료 시 드래그 상태 초기화
            isDragging = false;
        }
    }
}
