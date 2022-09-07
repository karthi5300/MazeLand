using System.Collections;
using UnityEngine;
using GlobalServices;
using UIServices;
using UnityEditor.MPE;

namespace PlayerServices
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] AudioClip m_playerMoveClip;
        [SerializeField] AudioClip m_coinCollectClip;

        private bool m_isMoving;
        private float m_rollSpeed = 3f;
        private int m_playerMoveCount = 25;
        private float m_distance = 1f;
        private bool m_noMovesLeft;
        private int m_playerCoinCount = 0;

        private void Start()
        {
            //when UI needs to be updated whenever a new level loads, invoke the event on start
            EventService.Instance.InvokeOnPlayerMove(m_playerMoveCount);
            EventService.Instance.InvokeOnPlayerCollectCoin(m_playerCoinCount);
        }


        // Update is called once per frame
        void Update()
        {
            if (m_isMoving)
                return;

            if (Input.GetKeyDown(KeyCode.W))
                CheckAndMove(Vector3.forward, m_distance);

            if (Input.GetKeyDown(KeyCode.S))
                CheckAndMove(Vector3.back, m_distance);

            if (Input.GetKeyDown(KeyCode.A))
                CheckAndMove(Vector3.left, m_distance);

            if (Input.GetKeyDown(KeyCode.D))
                CheckAndMove(Vector3.right, m_distance);
        }

        private void CheckAndMove(Vector3 direction, float distance)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, distance))
            {
                //if ray hits Wall, don't move the player
                if (hitInfo.collider.CompareTag("Wall"))
                    return;
            }

            //if ray not hits anything or hits anything other than wall, move player
            Assemble(direction);
            transform.rotation = Quaternion.identity;
        }

        void Assemble(Vector3 dir)
        {
            var anchor = transform.position + (Vector3.down + dir) * 0.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));

            DecreaseMovesLeft();

            if (m_playerMoveCount == 0)
                EventService.Instance.InvokeOnPlayerRunOutOfMoves();
        }
        IEnumerator Roll(Vector3 anchor, Vector3 axis)
        {
            //play player move sound, as soon as player moves
            AudioManager.Instance.Play(m_playerMoveClip);

            m_isMoving = true;
            for (int i = 0; i < (90 / m_rollSpeed); i++)
            {
                transform.RotateAround(anchor, axis, m_rollSpeed);
                yield return new WaitForSeconds(0.01f);
            }

            m_isMoving = false;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<LevelServices.CoinController>())
            {
                IncreasePlayerCoinCount();
                AudioManager.Instance.Play(m_coinCollectClip);
                Destroy(collider.gameObject);
            }

            if (collider.GetComponent<GamePlayServices.LevelOverController>())
            {
                DisplayLevelOverScreen();
            }

        }


        private void DecreaseMovesLeft()
        {
            m_playerMoveCount -= 1;

            //Invoke OnPlayerMove event when player moves
            EventService.Instance.InvokeOnPlayerMove(m_playerMoveCount);

            if (m_playerMoveCount == 0)
            {
                m_noMovesLeft = true;
                DisplayGameOverScreen();
            }
        }

        private void IncreasePlayerCoinCount()
        {
            m_playerCoinCount += 1;

            EventService.Instance.InvokeOnPlayerCollectCoin(m_playerCoinCount);
            //UIHandler.Instance.UpdateCoinCount(m_playerCoinCount);
            //UIHandler.Instance.UpdateScoreCount();
        }

        private void DisplayLevelOverScreen()
        {
            //UIHandler.Instance.DisplayLevelOver();
            EventService.Instance.InvokeOnPlayerCompleteLevel();
        }

        private void DisplayGameOverScreen()
        {
            //UIHandler.Instance.DisplayGameOver();
            EventService.Instance.InvokeOnPlayerRunOutOfMoves();
        }

    }
}