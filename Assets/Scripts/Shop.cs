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
    public void PurchaseAdvancedTurret () 
    {
        Debug.Log("Advanced Turret Selected");
        buildManager.SetTurretToBuild(buildManager.advancedTurretPrefab);
    }
}
