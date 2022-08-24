using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_rollSpeed = 3f;

    private bool m_isMoving;
    public System.Action OnPlayerMove;
    public System.Action OnPlayerCollectCoin;
    public System.Action OnPlayerCompleteLevel;

    // Update is called once per frame
    void Update()
    {
        if (m_isMoving)
            return;

        if (Input.GetKeyDown(KeyCode.A)) Assemble(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D)) Assemble(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W)) Assemble(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S)) Assemble(Vector3.back);

        void Assemble(Vector3 dir)
        {
            var anchor = transform.position + (Vector3.down + dir) * 0.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));

            if (OnPlayerMove != null)
                OnPlayerMove();
        }
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

            Destroy(collider.gameObject);
        }

        if (collider.GetComponent<LevelOverController>())
        {
            //invoke this event only when it is not null
            if (OnPlayerCompleteLevel != null)
                OnPlayerCompleteLevel();
        }
    }
}
