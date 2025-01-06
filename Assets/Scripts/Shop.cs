using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint cannonTurret;
    public TurretBlueprint laserTurret;
    public TurretBlueprint sniperTurret;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectCannonTurret()
    {
        Debug.Log("Cannon Turret Selected");
        buildManager.SelectTurretToBuild(cannonTurret);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Turret Selected");
        buildManager.SelectTurretToBuild(laserTurret);
    }

    public void SelectSniperTurret()
    {
        Debug.Log("Sniper Turret Selected");
        buildManager.SelectTurretToBuild(sniperTurret);
    }
}
