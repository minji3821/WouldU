using UnityEngine;

public class Gravity : MonoBehaviour
{
    private float rotateSpeed = 500f; // 회전 속도 조절용
    private Vector2 previousMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 드래그 시작 시 이전 마우스 위치 저장
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // 현재 마우스 위치
            Vector2 currentMousePosition = Input.mousePosition;

            // 드래그 방향 계산
            Vector2 delta = currentMousePosition - previousMousePosition;

            // 입력값 정규화
            float normalizedDeltaY = delta.y / Screen.height;
            float normalizedDeltaX = delta.x / Screen.width;

            // Z축 회전량 계산
            float verticalRotationAmount = normalizedDeltaY * rotateSpeed;
            float horizontalRotationAmount = normalizedDeltaX * rotateSpeed;

            // Z축 회전 적용 (상하 + 좌우)
            float zRotation = 0;

            // 상하 드래그에 따른 회전
            zRotation += (currentMousePosition.x > Screen.width / 2) ? verticalRotationAmount : -verticalRotationAmount;

            // 좌우 드래그에 따른 회전
            zRotation += (currentMousePosition.y > Screen.height / 2) ? -horizontalRotationAmount : horizontalRotationAmount;

            // Z축 회전 적용
            transform.Rotate(0, 0, zRotation);

            // 이전 위치 업데이트
            previousMousePosition = currentMousePosition;
        }
    }
}
