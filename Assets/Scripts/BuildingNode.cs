using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingNode : MonoBehaviour
{

    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start () 
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown () 
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
            
        if (buildManager.getTurretToBuild() == null)
            return;
        

        if (turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        //build a turret
        GameObject turretToBuild = BuildManager.instance.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
    void OnMouseEnter ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.getTurretToBuild() == null)
            return;
        
        rend.material.color = hoverColor;
    }

    void OnMouseExit () 
    {
        rend.material.color = startColor;
    }
}
