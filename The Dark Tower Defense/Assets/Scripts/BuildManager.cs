using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //singlton
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    private TurretBlueprint turretToBuild;

    public GameObject buildEffect;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool CanAfford { get { return turretToBuild.cost <= PlayerStats.Money; } }

    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.Money < turretToBuild.cost)
        {
            print("YOU HAVE NO MONEY");
            return;
        }

        GameObject turret = (GameObject)Instantiate(turretToBuild.turretPrefab, node.offset.position, node.offset.rotation);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.offset.position, Quaternion.identity);
        Destroy(effect, .5f);

        PlayerStats.Money -= turretToBuild.cost;
        print("Tower Build! Currency Left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBlueprint build)
    {
        turretToBuild = build;
    }
}

