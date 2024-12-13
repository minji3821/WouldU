using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingPanel;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private TextMeshProUGUI resultScoreText;
    [SerializeField]
    private Sprite[] BGImgs;
    [SerializeField]
    private SpriteRenderer BGImg;

    private bool isPanelActive = false;


    private void Start()
    {
        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

    public void ToggleSettingPanel()
    {
        if (settingPanel!= null)
        {
            isPanelActive = !isPanelActive;
            settingPanel.SetActive(isPanelActive);
        }
    }

    public void UpdateScoreUI(int newScore)
    {
        newScore = Mathf.Clamp(newScore, 0, 999999);
        if (scoreText != null)
            scoreText.text = newScore.ToString();
    }

    public void GoTOLobby()
    {
        SceneManager.LoadScene("StartScene");
        GameManager.Inst.isGaming = false;
    }

    public void ShowResult(int score)
    {
        resultPanel.SetActive(true);
        if (resultScoreText != null)
            resultScoreText.text = $"{score} Á¡";
    }

    public void ChangeBG(int Level)
    {
        BGImg.sprite = BGImgs[Level - 1];
    }

    public void InitSetting()
    {
        
    }
}
