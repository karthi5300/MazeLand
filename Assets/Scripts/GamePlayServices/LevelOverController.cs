using UnityEngine;

namespace GamePlayServices
{
    public class LevelOverController : MonoBehaviour
    {
        [SerializeField] AudioClip m_levelWinClip;

        void DisplayLevelOver()
        {
            //playing level win sound
            GlobalServices.AudioManager.Instance.Play(m_levelWinClip);
        }
    }
}