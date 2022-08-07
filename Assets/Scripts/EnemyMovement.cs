using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool mDynamicPathing = false;

    private Transform target;
    private int wavepointIndex = 0;

    void Start () 
    {
        target = Waypoints.points[0];
    }

    void Update () 
    {
        if (mDynamicPathing)
        {
            // recalc the path to the target destination
            // NOTE - we can probably do this more efficiently than every Update loop... (HasTerrainUpdated?)
            // NOTE - assuming the last endpoint is target destination

            // Implement A*?  probably
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
        }
    }

    void GetNextWaypoint() 
    {

        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

}
