using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] AudioClip m_levelLostClip;


    void Start()
    {
        FindObjectOfType<PlayerController>().OnPlayerRunOutOfMoves += DisplayGameOver;
    }

    void DisplayGameOver()
    {
        //playing level lost sound
        AudioManager.Instance.Play(m_levelLostClip);
    }
}
