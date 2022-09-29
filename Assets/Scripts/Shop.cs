using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret () 
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }
    public void PurchaseMissileTurret () 
    {
        Debug.Log("Missile Turret Selected");
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }
}
