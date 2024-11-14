using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private bool isDragging = false;
    private float rotationSpeed = 500f; // ȸ�� �ӵ� ������

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ��ġ ���� �� ��ġ ����
            startTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentTouchPosition = Input.mousePosition;
            Vector2 direction = currentTouchPosition - startTouchPosition;

            if (direction.magnitude > 5f) // �巡�� �ּ� �Ÿ�
            {
                bool isRightSide = currentTouchPosition.x > Screen.width / 2; // ���� ��ġ ��ġ�� �������� �Ǵ�
                float rotationAmount = rotationSpeed * Time.deltaTime;

                if (isRightSide)
                {
                    // ���� ��ġ�� ������ ȭ��
                    if (direction.y < 0) // �Ʒ������� �巡��
                    {
                        transform.Rotate(Vector3.forward, -rotationAmount); // �ð����
                    }
                    else if (direction.y > 0) // �������� �巡��
                    {
                        transform.Rotate(Vector3.forward, rotationAmount); // �ݽð����
                    }
                }
                else
                {
                    // ���� ��ġ�� ���� ȭ��
                    if (direction.y < 0) // �Ʒ������� �巡��
                    {
                        transform.Rotate(Vector3.forward, rotationAmount); // �ݽð����
                    }
                    else if (direction.y > 0) // �������� �巡��
                    {
                        transform.Rotate(Vector3.forward, -rotationAmount); // �ð����
                    }
                }

                // ���� ��ġ ������Ʈ
                startTouchPosition = currentTouchPosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ��ġ ���� �� �巡�� ���� �ʱ�ȭ
            isDragging = false;
        }
    }
}
