using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_rollSpeed = 3f;
    [SerializeField] AudioClip m_playerMoveClip;
    [SerializeField] AudioClip m_coinCollectClip;

    private bool m_isMoving;
    private int m_playerMoveCount = 10;
    private bool m_moveLeft;
    private bool m_moveRight;
    private bool m_moveForward;
    private bool m_moveBack;

    public System.Action OnPlayerMove;
    public System.Action OnPlayerCollectCoin;
    public System.Action OnPlayerCompleteLevel;
    public System.Action OnPlayerRunOutOfMoves;

    // Update is called once per frame
    void Update()
    {
        if (m_isMoving)
            return;

        if (m_moveLeft) { if (Input.GetKeyDown(KeyCode.A)) Assemble(Vector3.left); }

        if (m_moveRight) { if (Input.GetKeyDown(KeyCode.D)) Assemble(Vector3.right); }

        if (m_moveForward) { if (Input.GetKeyDown(KeyCode.W)) Assemble(Vector3.forward); }

        if (m_moveBack) { if (Input.GetKeyDown(KeyCode.S)) Assemble(Vector3.back); }

        transform.rotation = Quaternion.identity;

        CheckBorder();
    }

    void Assemble(Vector3 dir)
    {
        var anchor = transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(Roll(anchor, axis));

        if (OnPlayerMove != null)
            OnPlayerMove();

        if (--m_playerMoveCount == 0)
            if (OnPlayerRunOutOfMoves != null)
                OnPlayerRunOutOfMoves();
    }


    IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
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
        if (collider.GetComponent<CoinController>())
        {
            //invoke this event only when it is not null
            if (OnPlayerCollectCoin != null)
                OnPlayerCollectCoin();

            AudioManager.Instance.Play(m_coinCollectClip);
            Destroy(collider.gameObject);
        }

        if (collider.GetComponent<LevelOverController>())
        {
            //invoke this event only when it is not null
            if (OnPlayerCompleteLevel != null)
                OnPlayerCompleteLevel();
        }

    }

    void CheckBorder()
    {
        //creating the ray
        Ray forwardRay = new Ray(transform.position, Vector3.forward);
        Ray backRay = new Ray(transform.position, Vector3.back);
        Ray leftRay = new Ray(transform.position, Vector3.left);
        Ray rightRay = new Ray(transform.position, Vector3.right);

        //creating variable for object that gets hit by the ray
        RaycastHit hitInfo;

        //Raycast function to check for the ray being hit
        if (Physics.Raycast(leftRay, out hitInfo, 1))
        {
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
                m_moveLeft = false;
        }
        else
            m_moveLeft = true;

        if (Physics.Raycast(rightRay, out hitInfo, 1))
        {
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
                m_moveRight = false;
        }
        else
            m_moveRight = true;

        if (Physics.Raycast(forwardRay, out hitInfo, 1))
        {
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
                m_moveForward = false;
        }
        else
            m_moveForward = true;

        if (Physics.Raycast(backRay, out hitInfo, 1))
        {
            if (hitInfo.collider.gameObject.CompareTag("Wall"))
                m_moveBack = false;
        }
        else
            m_moveBack = true;

    }
}
