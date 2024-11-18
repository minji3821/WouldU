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
    }
}
