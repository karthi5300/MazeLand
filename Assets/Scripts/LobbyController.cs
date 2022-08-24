using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private AudioClip m_lobbyMusicClip;
    [SerializeField] private AudioClip m_gameMusicClip;

    private const int m_firstSceneBuildIndex = 1;

    private void Start()
    {
        //playing lobby music
        AudioManager.Instance.PlayMusic(m_lobbyMusicClip);
    }

    public void OnClickPlayButton()
    {
        
        SceneManager.LoadScene(m_firstSceneBuildIndex);
        //playing lobby music
        AudioManager.Instance.PlayMusic(m_gameMusicClip);
    }

    public void OnClickOptionsButton()
    {
        Debug.Log("Options");
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
