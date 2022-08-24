using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(1);
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
