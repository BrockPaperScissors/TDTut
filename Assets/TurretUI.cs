using UnityEngine;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;
    private BuildingNode target;

    public void SetTarget (BuildingNode _target)
    {
        target = _target;
        
        transform.position = target.GetBuildPosition();
        ui.SetActive(true);
    }

    public void Hide () 
    {
        ui.SetActive(false);
    }
}
