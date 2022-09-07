using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using GlobalServices;

namespace UIServices
{
    public class UIHandler : MonoSingletonGeneric<UIHandler>
    {
        [SerializeField] private TextMeshProUGUI m_playerMovesLeft;
        [SerializeField] private TextMeshProUGUI m_currentCoins;
        [SerializeField] private TextMeshProUGUI m_finalCoins;
        [SerializeField] private TextMeshProUGUI m_currentScore;
        [SerializeField] private TextMeshProUGUI m_finalScore;
        [SerializeField] private GameObject m_gameOverScreen;
        [SerializeField] private GameObject m_levelOverScreen;
        [SerializeField] AudioClip m_levelWinClip;
        [SerializeField] AudioClip m_levelLostClip;

        int m_moveCount;
        int m_coinCount;
        int m_score;
        int m_currentScene;

        bool m_isGameOver = false;
        bool m_isLevelOver = false;

        // Start is called before the first frame update
        void Start()
        {
            m_currentScene = SceneManager.GetActiveScene().buildIndex;
            m_playerMovesLeft.text = "Moves left : " + m_moveCount;
            m_currentCoins.text = "Coins : " + m_coinCount;

            EventService.Instance.OnPlayerMove += UpdateMovesLeft;
            EventService.Instance.OnPlayerCollectCoin += UpdateCoinCount;
            EventService.Instance.OnPlayerCompleteLevel += DisplayLevelOver;
            EventService.Instance.OnPlayerRunOutOfMoves += DisplayGameOver;
        }

        private void Update()
        {
            if (m_isGameOver)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_isGameOver = false;
                    m_gameOverScreen.SetActive(false);
                    m_finalCoins.gameObject.SetActive(false);
                    m_finalScore.gameObject.SetActive(false);
                    SceneManager.LoadScene(m_currentScene);
                }

            if (m_isLevelOver)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_isLevelOver = false;
                    m_levelOverScreen.SetActive(false);
                    m_finalCoins.gameObject.SetActive(false);
                    m_finalScore.gameObject.SetActive(false);
                    SceneManager.LoadScene(++m_currentScene);
                }
        }

        public void UpdateMovesLeft(int p_moveCount)
        {
            m_moveCount = p_moveCount;
            m_playerMovesLeft.text = "Moves left : " + p_moveCount;
        }

        public void UpdateCoinCount(int p_coinCount)
        {
            m_currentCoins.text = "Coins : " + p_coinCount;
            UpdateScoreCount();
        }

        public void UpdateScoreCount()
        {
            if (m_moveCount > 5) m_score += 100;
            else if (m_moveCount > 3) m_score += 50;
            else m_score += 25;

            m_currentScore.text = "Score : " + m_score.ToString();
            PlayerPrefs.SetInt("TOTAL_SCORE", m_score);
        }

        public void DisplayGameOver()
        {
            //displaying you lost screen
            m_gameOverScreen.SetActive(true);

            //updating UI
            DisplayFinalScore();
            m_finalCoins.text = m_currentCoins.text;
            m_finalScore.text = m_currentScore.text;
            m_isGameOver = true;
            AudioManager.Instance.Play(m_levelLostClip);
        }

        public void DisplayLevelOver()
        {
            //displaying you win screen
            m_levelOverScreen.SetActive(true);

            //updating UI
            DisplayFinalScore();
            m_finalCoins.text = m_currentCoins.text;
            m_finalScore.text = m_currentScore.text;
            m_isLevelOver = true;
            AudioManager.Instance.Play(m_levelWinClip);
        }

        public void DisplayFinalScore()
        {
            m_finalCoins.gameObject.SetActive(true);
            m_finalScore.gameObject.SetActive(true);
        }

    }
}