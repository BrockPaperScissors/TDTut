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
    }
}
