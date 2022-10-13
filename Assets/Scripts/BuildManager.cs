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
    private TurretBlueprint turretToBuild;

    // void Start () 
    // {
    //     turretToBuild = standardTurretPrefab;
    // }

    public bool CanBuild { get {return turretToBuild != null; }}

    public void BuildTurretOn (BuildingNode buildingNode)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that.");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        Debug.Log(PlayerStats.Money);
        //build a turret
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, buildingNode.GetBuildPosition(), Quaternion.identity);
        buildingNode.turret = turret;

    }

    public void SelectTurretToBuild( TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
