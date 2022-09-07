using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LevelServices
{

    public class MovingPlatformController : MonoBehaviour
    {
        [SerializeField] private WaypointPath m_waypointPath;
        [SerializeField] private float m_speed;
        [SerializeField] private GameObject m_wall1;
        [SerializeField] private GameObject m_wall2;

        private int m_targetWaypointIndex;
        private Transform m_previousWaypoint;
        private Transform m_targetWaypoint;

        private float m_timeToWaypoint;
        private float m_elapsedTime;
        private bool m_isWallActive = true;


        // Start is called before the first frame update
        void Start()
        {
            TargetNextWaypoint();
        }

        // Update is called once per frame
        void Update()
        {
            m_elapsedTime += Time.deltaTime;

            float elapasedPercentage = m_elapsedTime / m_timeToWaypoint;
            elapasedPercentage = Mathf.SmoothStep(0, 1, elapasedPercentage);
            transform.position = Vector3.Lerp(m_previousWaypoint.position, m_targetWaypoint.position, elapasedPercentage);

            if (elapasedPercentage >= 1)
                TargetNextWaypoint();
        }

        private void TargetNextWaypoint()
        {
            m_previousWaypoint = m_waypointPath.GetWayPoint(m_targetWaypointIndex);
            m_targetWaypointIndex = m_waypointPath.GetNextWaypointIndex(m_targetWaypointIndex);
            m_targetWaypoint = m_waypointPath.GetWayPoint(m_targetWaypointIndex);

            m_elapsedTime = 0;

            float distanceToWaypoint = Vector3.Distance(m_previousWaypoint.position, m_targetWaypoint.position);
            m_timeToWaypoint = distanceToWaypoint / m_speed;
        }
        /*
            void OnTriggerEnter(Collider other)
            {
                Debug.Log("working");
                if (other.CompareTag("Wall"))
                {
                    if (m_isWallActive)
                    {
                        Debug.Log("wall detected");
                        other.GetComponent<BoxCollider>().enabled = false;
                        m_isWallActive = false;
                    }
                    else
                    {
                        Debug.Log("no wall detected");
                        other.GetComponent<BoxCollider>().enabled = true;
                        m_isWallActive = true;
                    }
                }


                other.transform.SetParent(transform);
            }
        */

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("working");
        }

        private void OnTriggerExit(Collider other)
        {
            other.transform.SetParent(null);
        }
    }
}