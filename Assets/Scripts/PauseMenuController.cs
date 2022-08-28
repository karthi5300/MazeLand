using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button m_pauseButton;
    [SerializeField] private Button m_resumeButton;
    [SerializeField] private GameObject m_pauseMenuScreen;

    private int LOBBY_SCENE_INDEX = 0;

    public void OnClickPauseButton()
    {
        m_pauseButton.gameObject.SetActive(false);
        m_pauseMenuScreen.gameObject.SetActive(true);
        m_resumeButton.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickResumeButton()
    {
        m_resumeButton.gameObject.SetActive(false);
        m_pauseMenuScreen.gameObject.SetActive(false);
        m_pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickOptionsButton()
    {
        Debug.Log("Options");
    }

    public void OnclickMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(LOBBY_SCENE_INDEX);
    }
}
