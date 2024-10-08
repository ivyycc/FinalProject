using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool hasStolenItem = false;
    public Transform[] waypoints; // Array of empty GameObjects
    private int currentWaypointIndex = 0;
    private float step = 2f; // Adjust the speed as needed

    void Update()
    {
        if (hasStolenItem && currentWaypointIndex < waypoints.Length)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }
}
