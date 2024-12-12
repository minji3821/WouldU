using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewUIManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> previewImages; // 3개의 미리보기 UI 이미지

    public void UpdatePreview(List<Sprite> sprites)
    {
        for (int i = 0; i < previewImages.Count; i++)
        {
            if (i < sprites.Count)
            {
                previewImages[i].sprite = sprites[i];
                previewImages[i].SetNativeSize();
                previewImages[i].transform.localScale = Vector3.one * 0.4f; // 크기 0.5로 조정
            }
            else
            {
                // 남은 칸 초기화
                previewImages[i].sprite = null;
            }
        }
    }
}
