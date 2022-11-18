using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Text sellAmount;
    public Button upgradeButton;
    private BuildingNode target;

    public void SetTarget (BuildingNode _target)
    {
        target = _target;
        
        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        } else 
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
        ui.SetActive(true);
    }

    public void Hide () 
    {
        ui.SetActive(false);
    }

    public void Upgrade ()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectBuildingNode();
    }

    public void Sell ()
    {
        target.SellTurret();
        BuildManager.instance.DeselectBuildingNode();
    }
}
