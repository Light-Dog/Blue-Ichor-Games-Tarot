using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Transform offset;

    [Header("Resource Zone")]
    public bool hasResource = false;
    public Resource resouce;
    public TextMeshPro resourceText;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    //cacheing data
    private Renderer rend;
    private Color startColor;
    private BuildManager buildManger;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        if (hasResource)
            rend.material.color = resouce.resourceColor;

        buildManger = BuildManager.instance;
    }

    private void Update()
    {
        if(hasResource)
        {
            resourceText.text = "x" + resouce.resourceCount;

            if(!resouce.ResourceCheck())
            {
                startColor = Color.white;

                hasResource = false;
                resourceText.enabled = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if(hasResource)
        {
            FindObjectOfType<PlayerStats>().SummonWorker(resouce, offset);
            return;
        }

        if(turret != null)
        {
            Debug.Log("Turrent Already built here!");
            buildManger.SelectNode(this);
        }

        if (!buildManger.CanBuild)
            return;

        turretBlueprint = buildManger.GetTurretToBuild();
        BuildTurret();
    }

    void BuildTurret()
    {
        if (PlayerStats.Money < turretBlueprint.cost)
        {
            print("YOU HAVE NO MONEY");
            return;
        }

        GameObject turret_temp = (GameObject)Instantiate(turretBlueprint.turretPrefab, offset.position, offset.rotation);
        turret = turret_temp;

        GameObject effect = (GameObject)Instantiate(buildManger.buildEffect, offset.position, Quaternion.identity);
        Destroy(effect, .5f);

        PlayerStats.Money -= turretBlueprint.cost;
        print("Tower Built! Currency Left: " + PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        //if the player has the money
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            print("YOU HAVE NO MONEY");
            return;
        }

        //apply upgrade, instantiate the effect, possibly build new tower
        Destroy(turret);

        GameObject turret_temp = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, offset.position, offset.rotation);
        turret = turret_temp;

        GameObject effect = (GameObject)Instantiate(buildManger.buildEffect, offset.position, Quaternion.identity);
        Destroy(effect, .5f);

        //subtract upgrade cost
        PlayerStats.Money -= turretBlueprint.upgradeCost;
        print("Tower Upgraded! Currency Left: " + PlayerStats.Money);
        turretBlueprint.upgraded = true;
    }

    private void OnMouseEnter()
    {
        if (hasResource)
        {
            if(PlayerStats.Lives > 1)
            {
                rend.material.color = resouce.resouceHoverColor;
                return;
            }
        }

        if (!buildManger.CanBuild)
            return;

        if(buildManger.CanAfford)
            rend.material.color = hoverColor;
        else
            rend.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        if (hasResource)
            rend.material.color = resouce.resourceColor;
        else
            rend.material.color = startColor;

    }
}
