using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UIServices
{
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
            m_pauseButton.gameObject.SetActive(true);
            m_pauseMenuScreen.gameObject.SetActive(false);
            m_resumeButton.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public void OnClickRestartButton()
        {
            Time.timeScale = 1f;
            m_pauseMenuScreen.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnclickMainMenuButton()
        {
            Time.timeScale = 1f;
            m_pauseMenuScreen.gameObject.SetActive(false);
            SceneManager.LoadScene(LOBBY_SCENE_INDEX);
        }
    }
}