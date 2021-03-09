using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public TurretBlueprint standardTarget;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint lazerBeam;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        print("Puchased Turret");
        buildManager.SelectTurretToBuild(standardTarget);
    }

    public void SelectMissileLauncher()
    {
        print("Puchased Missle");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLazerBeam()
    {
        print("Puchased Lazer");
        buildManager.SelectTurretToBuild(lazerBeam);
    }
}
