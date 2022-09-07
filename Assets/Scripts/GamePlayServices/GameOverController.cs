using UnityEngine;
using PlayerServices;

namespace GamePlayServices
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] AudioClip m_levelLostClip;

        public void DisplayGameOver()
        {
            //playing level lost sound
            GlobalServices.AudioManager.Instance.Play(m_levelLostClip);
        }
    }
}