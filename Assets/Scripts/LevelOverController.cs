using UnityEngine;

public class LevelOverController : MonoBehaviour
{
    [SerializeField] AudioClip m_levelWinClip;

    private void Start()
    {
        FindObjectOfType<PlayerController>().OnPlayerCompleteLevel += DisplayLevelOver;
    }

    void DisplayLevelOver()
    {
        //playing level win sound
        AudioManager.Instance.Play(m_levelWinClip);
    }
}
