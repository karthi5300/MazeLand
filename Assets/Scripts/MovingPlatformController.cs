using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] private WaypointPath m_waypointPath;
    [SerializeField] private float m_speed;

    private int m_targetWaypointIndex;
    private Transform m_previousWaypoint;
    private Transform m_targetWaypoint;

    private float m_timeToWaypoint;
    private float m_elapsedTime;


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
}
