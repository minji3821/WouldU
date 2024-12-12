using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject exitPopUp;
    [SerializeField]
    private GameObject howToPlayPopUp;

    public void ToggleExitPopUP(bool active)
    {
        exitPopUp.SetActive(active);
    }

    public void ToggleHowToPlayPopUp(bool active)
    {
        howToPlayPopUp.SetActive(active);
    }

    public void OnClick_ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
