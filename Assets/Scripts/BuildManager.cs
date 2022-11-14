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

    public GameObject buildEffect;
    private TurretBlueprint turretToBuild;
    private BuildingNode selectedTurret;

    public TurretUI turretUI;

    public bool CanBuild { get {return turretToBuild != null; }}
    public bool HasMoney { get {return PlayerStats.Money >= turretToBuild.cost; }}

    public void SelectBuildingNode (BuildingNode buildingNode)
    {
        if (selectedTurret == buildingNode)
        {
            DeselectBuildingNode();
            return;
        }
        selectedTurret = buildingNode;
        turretToBuild = null;

        turretUI.SetTarget (buildingNode);
    }

    public void DeselectBuildingNode()
    {
        selectedTurret = null;
        turretUI.Hide();
    }

    public void SelectTurretToBuild( TurretBlueprint turret)
    {
        turretToBuild = turret;

        DeselectBuildingNode();
    }

    public TurretBlueprint GetTurretToBuild ()
    {
        return turretToBuild;
    }
}
