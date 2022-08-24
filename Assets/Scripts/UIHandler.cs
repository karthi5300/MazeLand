using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
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

    int m_moveCount = 10;
    int m_coinCount = 0;
    int m_score;
    int m_currentScene;

    bool m_noMovesLeft = false;
    bool m_isGameOver = false;
    bool m_isLevelOver = false;

    // Start is called before the first frame update
    void Start()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;
        m_playerMovesLeft.text = "Moves left : " + m_moveCount;
        m_currentCoins.text = "Coins : " + m_coinCount;

        //subscribing OnGameOver method to OnPlayerDeath event
        FindObjectOfType<PlayerController>().OnPlayerMove += DecreaseMoveCount;
        FindObjectOfType<PlayerController>().OnPlayerCollectCoin += UpdateCoinCount;
        FindObjectOfType<PlayerController>().OnPlayerCompleteLevel += DisplayLevelOver;
    }

    private void Update()
    {
        if (m_isLevelOver)
            DisplayLevelOver();

        if (m_noMovesLeft)
            DisplayGameOver();

        if (m_isGameOver)
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(m_currentScene);

        if (m_isLevelOver)
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(++m_currentScene);
    }

    //since we subscribed DecreaseMoveCount method to OnPlayerMove event,
    //this method will be executed when player moves
    void DecreaseMoveCount()
    {
        m_moveCount--;
        m_playerMovesLeft.text = "Moves left : " + m_moveCount;

        if (m_moveCount == 0)
            m_noMovesLeft = true;
    }

    //since we subscribed IncreaseCoinCount method to OnPlayerCollectCoin event,
    //this method will be executed when player collides with coin
    void UpdateCoinCount()
    {
        m_coinCount++;
        m_currentCoins.text = "Coins : " + m_coinCount;

        UpdateScoreCount();
    }

    void UpdateScoreCount()
    {
        if (m_moveCount > 5) m_score += 100;
        else if (m_moveCount > 3) m_score += 50;
        else m_score += 25;

        m_currentScore.text = "Score : " + m_score.ToString();
    }

    void DisplayGameOver()
    {
        //displaying you lost screen
        m_gameOverScreen.SetActive(true);

        //updating UI
        DisplayFinalCoinScore();
        m_finalCoins.text = m_currentCoins.text;
        m_finalScore.text = m_currentScore.text;
        m_isGameOver = true;
    }

    void DisplayLevelOver()
    {
        //displaying you win screen
        m_levelOverScreen.SetActive(true);

        //updating UI
        DisplayFinalCoinScore();
        m_finalCoins.text = m_currentCoins.text;
        m_finalScore.text = m_currentScore.text;
        m_isLevelOver = true;
    }

    void DisplayFinalCoinScore()
    {
        m_finalCoins.gameObject.SetActive(true);
        m_finalScore.gameObject.SetActive(true);
    }

}
