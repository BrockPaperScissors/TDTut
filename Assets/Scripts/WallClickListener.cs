using UnityEngine;

public class WallClickListener : MonoBehaviour
{
        void OnMouseDown()
    {        
        // Just doing this to show that we have disabled this wall
        // We should eventually do something that makes more sense to the player
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = !renderer.enabled;
        }

        PathFindingManager manager = new PathFindingManager();
        //manager.Init(10, 10);
        //manager.FindPath(new Vector2Int(1,1), new Vector2Int(3,2));
        //manager.FindPath(new Vector2Int(2,2), new Vector2Int(0,5));

        /*for (int x = 15; x >= 0; --x)
        {
            for (int y = 0; y <= 15; --y)

            {
                                
                GameObject node = GameObject.Find("Node");        
                manager.SetNodeData(x, y, false);
            }
        }*/

        char[,] carray = new char[16,16];
        for (int x = 0; x <= 15; ++x)
        {
            for (int y = 0; y <= 15; ++y)
            {
                carray[x,y] = '-';
            }
        }


        manager.Init(16, 16);
        GameObject nodes = GameObject.Find("Nodes");
        foreach (Transform node in nodes.GetComponentInChildren<Transform>())
        {
            // bottom right of map is 0,0
            // and "x" is the z axis
            // and "y" is the x axis

            // this grid construction is jank, can't resize
            float x = 15 - (node.transform.position.z / 4.5f);
            float y = node.transform.position.x / 4.75f;
            if (node.name == "Node (41)")
            {
                int b = 0;
                b++;
            }
            MeshRenderer r = node.GetComponent<MeshRenderer>();
            bool passable = r.enabled; // TODO - I need to flip in init for testing
            
            manager.SetNodeData(Mathf.RoundToInt(x), Mathf.RoundToInt(y), passable);
            if (passable)
            {
                carray[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] = 'X';
            }
        }

        bool stophere = true;
        stophere = !stophere;

        for (int y = 15; y >= 0; --y)
        {
            string s = "";
            for (int x = 0; x <= 15; ++x)
            {
                s += carray[x, y];
            }
            Debug.Log(s);
        }

        // starts at (x, 0), goes to (0, y)
        /*for (int x = 15; x >= 0; --x)
        {
            for (int y = 0; y <= 15; --y)

            {
                                
                GameObject node = GameObject.Find("Node");        
                manager.SetNodeData(x, y, false);
            }
        }*/
        //GameObject nodes = GameObject.Find("Nodes");
        //foreach(GameObject child in nodes.GetComponentInChildren)
        //{

        //}

        // How can I unit test this stuff?
        // It'd be really nice to visualize this stuff
    }
}
