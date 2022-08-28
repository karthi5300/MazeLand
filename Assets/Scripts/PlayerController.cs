using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_rollSpeed = 3f;
    [SerializeField] AudioClip m_playerMoveClip;
    [SerializeField] AudioClip m_coinCollectClip;

    private bool m_isMoving;
    private int m_playerMoveCount = 25;
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

        if (m_moveLeft)
            if (Input.GetKeyDown(KeyCode.A))
            {
                Assemble(Vector3.left);
                //resetting the raycast direction despite the player's rotation
                transform.rotation = Quaternion.identity;
            }

        if (m_moveRight)
            if (Input.GetKeyDown(KeyCode.D))
            {
                Assemble(Vector3.right);
                //resetting the raycast direction despite the player's rotation
                transform.rotation = Quaternion.identity;
            }

        if (m_moveForward)
            if (Input.GetKeyDown(KeyCode.W))
            {
                Assemble(Vector3.forward);
                //resetting the raycast direction despite the player's rotation
                transform.rotation = Quaternion.identity;
            }

        if (m_moveBack)
            if (Input.GetKeyDown(KeyCode.S))
            {
                Assemble(Vector3.back);
                //resetting the raycast direction despite the player's rotation
                transform.rotation = Quaternion.identity;
            }

        //transform.rotation = Quaternion.identity;
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
        if (Physics.Raycast(leftRay, out hitInfo, 1f))
        {
            Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * 1, Color.red);

            if (hitInfo.collider.gameObject.CompareTag("Wall"))     //when wall detected, don't allow player to move
                m_moveLeft = false;
            else
                m_moveLeft = true;

            /*            
            if (hitInfo.collider.gameObject.CompareTag("Ladder"))   //when ladder detected, allow player to move
                            m_moveLeft = true;*/
        }
        else
        {
            m_moveLeft = true;                                      //when no obstacle detected, allow player to move
            Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * 1, Color.green);
        }

        if (Physics.Raycast(rightRay, out hitInfo, 1f))
        {
            Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * 1, Color.red);

            if (hitInfo.collider.gameObject.CompareTag("Wall"))     //when wall detected, don't allow player to move
                m_moveRight = false;
            else
                m_moveRight = true;

            /*
            if (hitInfo.collider.gameObject.CompareTag("Ladder"))   //when ladder detected, allow player to move
                            m_moveRight = true;*/
        }
        else
        {
            m_moveRight = true;                                     //when no obstacle detected, allow player to move
            Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * 1, Color.green);
        }
        if (Physics.Raycast(forwardRay, out hitInfo, 1f))
        {
            Debug.DrawLine(forwardRay.origin, forwardRay.origin + forwardRay.direction * 1, Color.red);

            if (hitInfo.collider.gameObject.CompareTag("Wall"))     //when wall detected, don't allow player to move
                m_moveForward = false;
            else
                m_moveForward = true;

            /*
            if (hitInfo.collider.gameObject.CompareTag("Ladder"))   //when ladder detected, allow player to move
                m_moveForward = true;*/
        }
        else
        {
            m_moveForward = true;                                     //when no obstacle detected, allow player to move
            Debug.DrawLine(forwardRay.origin, forwardRay.origin + forwardRay.direction * 1, Color.green);
        }

        if (Physics.Raycast(backRay, out hitInfo, 1f))
        {
            Debug.DrawLine(backRay.origin, backRay.origin + backRay.direction * 1, Color.red);

            if (hitInfo.collider.gameObject.CompareTag("Wall"))     //when wall detected, don't allow player to move
                m_moveBack = false;
            else
                m_moveBack= true;

            /*            
            if (hitInfo.collider.gameObject.CompareTag("Ladder"))   //when ladder detected, allow player to move
                            m_moveBack = true;*/
        }
        else
        {
            m_moveBack = true;                                     //when no obstacle detected, allow player to move
            Debug.DrawLine(backRay.origin, backRay.origin + backRay.direction * 1, Color.green);
        }
    }
}
