using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewUIManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> previewImages; // 3���� �̸����� UI �̹���

    public void UpdatePreview(List<Sprite> sprites)
    {
        for (int i = 0; i < previewImages.Count; i++)
        {
            if (i < sprites.Count)
            {
                previewImages[i].sprite = sprites[i];
                previewImages[i].SetNativeSize();
                previewImages[i].transform.localScale = Vector3.one * 0.4f; // ũ�� 0.5�� ����
            }
            else
            {
                // ���� ĭ �ʱ�ȭ
                previewImages[i].sprite = null;
            }
        }
    }
}
