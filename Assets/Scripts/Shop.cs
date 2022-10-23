using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret () 
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectMissileTurret () 
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer ()
    {
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
