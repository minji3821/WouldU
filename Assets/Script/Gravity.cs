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
                bool isRightSide = currentTouchPosition.x > Screen.width / 2; // 현재 터치 위치를 기준으로 판단
                float rotationAmount = rotationSpeed * Time.deltaTime;

                if (isRightSide)
                {
                    // 현재 위치가 오른쪽 화면
                    if (direction.y < 0) // 아래쪽으로 드래그
                    {
                        transform.Rotate(Vector3.forward, -rotationAmount); // 시계방향
                    }
                    else if (direction.y > 0) // 위쪽으로 드래그
                    {
                        transform.Rotate(Vector3.forward, rotationAmount); // 반시계방향
                    }
                }
                else
                {
                    // 현재 위치가 왼쪽 화면
                    if (direction.y < 0) // 아래쪽으로 드래그
                    {
                        transform.Rotate(Vector3.forward, rotationAmount); // 반시계방향
                    }
                    else if (direction.y > 0) // 위쪽으로 드래그
                    {
                        transform.Rotate(Vector3.forward, -rotationAmount); // 시계방향
                    }
                }

                // 시작 위치 업데이트
                startTouchPosition = currentTouchPosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 터치 종료 시 드래그 상태 초기화
            isDragging = false;
        }
    }
}
