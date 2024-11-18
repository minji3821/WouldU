using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingPanel;

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

    public void GoTOLobby()
    {
        SceneManager.LoadScene("StartScene");
    }
}
