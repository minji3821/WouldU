using UnityEngine;

public class Gravity : MonoBehaviour
{
    private float rotateSpeed = 500f; // ȸ�� �ӵ� ������
    private Vector2 previousMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �巡�� ���� �� ���� ���콺 ��ġ ����
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // ���� ���콺 ��ġ
            Vector2 currentMousePosition = Input.mousePosition;

            // �巡�� ���� ���
            Vector2 delta = currentMousePosition - previousMousePosition;

            // �Է°� ����ȭ
            float normalizedDeltaY = delta.y / Screen.height;
            float normalizedDeltaX = delta.x / Screen.width;

            // Z�� ȸ���� ���
            float verticalRotationAmount = normalizedDeltaY * rotateSpeed;
            float horizontalRotationAmount = normalizedDeltaX * rotateSpeed;

            // Z�� ȸ�� ���� (���� + �¿�)
            float zRotation = 0;

            // ���� �巡�׿� ���� ȸ��
            zRotation += (currentMousePosition.x > Screen.width / 2) ? verticalRotationAmount : -verticalRotationAmount;

            // �¿� �巡�׿� ���� ȸ��
            zRotation += (currentMousePosition.y > Screen.height / 2) ? -horizontalRotationAmount : horizontalRotationAmount;

            // Z�� ȸ�� ����
            transform.Rotate(0, 0, zRotation);

            // ���� ��ġ ������Ʈ
            previousMousePosition = currentMousePosition;
        }
    }
}
