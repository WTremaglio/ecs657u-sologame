using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
    }
}
