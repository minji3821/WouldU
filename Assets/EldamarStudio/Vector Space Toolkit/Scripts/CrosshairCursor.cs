using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;
    }
}