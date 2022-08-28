using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWayPoint(int p_waypointIndex)
    {
        return transform.GetChild(p_waypointIndex);
    }

    public int GetNextWaypointIndex(int p_currentWaypointIndex)
    {
        int nextWaypointIndex = p_currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
            nextWaypointIndex = 0;

        return nextWaypointIndex;
    }
}
