using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake () 
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    private GameObject turretToBuild;

    void Start () 
    {
        turretToBuild = standardTurretPrefab;
    }
    public GameObject getTurretToBuild () 
    {
        return turretToBuild;
    }

    public void SetTurretToBuild (GameObject turret)
    {
        turretToBuild = turret;
    }
}
