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
    [SerializeField]
    private CircularBoundary CB;

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

            if (isPanelActive)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
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
        GameManager.Inst.InitSetting();
    }

    public void ShowResult(int score)
    {
        resultPanel.SetActive(true);
        if (resultScoreText != null)
            resultScoreText.text = $"{score} ��";
    }

    public void ChangeBG(int Level)
    {
        BGImg.sprite = BGImgs[Level - 1];
    }

    public void InitSetting()
    {
        resultPanel.SetActive(false);
        if (scoreText != null)
            scoreText.text = "0";

        CB.InitSetting();
    }

    public void ReStart()
    {
        GameManager.Inst.ReStart();
    }
}
