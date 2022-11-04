using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100f;
    public int moneyValue = 50;
    public GameObject deathEffect;


    public bool mDynamicPathing = false;

    private Transform target;
    private Transform target2;
    private int wavepointIndex = 0;
    private int wavepointIndex2 = 0;

    PathFindingManager mManager;
    List<Vector3> mDynamicWaypoints;

    void Start () 
    {
        speed = startSpeed;
        target = Waypoints.points[0];

        mManager = new PathFindingManager();
        mManager.Init(16, 16);
        GameObject nodes = GameObject.Find("Nodes");
        foreach (Transform node in nodes.GetComponentInChildren<Transform>())
        {
            // bottom right of map is 0,0
            // and "x" is the z axis
            // and "y" is the x axis

            // this grid construction is jank, can't resize
            float x = 15 - (node.transform.position.z / 4.5f);
            float y = node.transform.position.x / 4.75f;
            //these are all walls...
            //MeshRenderer r = node.GetComponent<MeshRenderer>();
            bool passable = false; //r.enabled;
            
            mManager.SetNodeData(Mathf.RoundToInt(x), Mathf.RoundToInt(y), passable);
        }
        mDynamicWaypoints = mManager.FindPath2(new Vector2Int(14, 2), new Vector2Int(1, 14)); // should be real world

        //UNCOMMENT THIS FOR DYNAMIC PATHING
        //target2 = Waypoints.points[0];
        //target2.position = mDynamicWaypoints[0];
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow (float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    void Die () 
    {
        PlayerStats.Money += moneyValue;
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    void Update () 
    {
        if (mDynamicPathing)
        {
            // recalc the path to the target destination
            // NOTE - we can probably do this more efficiently than every Update loop... (HasTerrainUpdated?)
            // NOTE - assuming the last endpoint is target destination

            // Implement A*?  probably

           

            Debug.DrawLine(transform.position, target2.position, Color.green);
            Vector3 dir = target2.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target2.position) <= 0.4f)
            {
                GetNextWaypoint2();
            }

            speed = startSpeed;

        }
        else
        {   
            Debug.DrawLine(transform.position, target.position, Color.green);
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
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

        void GetNextWaypoint2() 
    {

        if (wavepointIndex2 >= mDynamicWaypoints.Count - 1)
        {
            //Destroy(gameObject);
            return;
        }

        wavepointIndex2++;
        target2.position = mDynamicWaypoints[wavepointIndex2];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

}
