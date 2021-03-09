using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Transform offset;

    [Header("Resource Zone")]
    public bool hasResource = false;
    public Resource resouce;
    //link to text object

    [Header("Optional")]
    public GameObject turret;

    //cacheing data
    private Renderer rend;
    private Color startColor;
    private BuildManager buildManger;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManger = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if(hasResource)
        {

        }

        if (!buildManger.CanBuild)
            return;

        if(turret != null)
        {
            Debug.Log("Turrent Already built here!");
        }

        buildManger.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (hasResource)
        {
            if(PlayerStats.Lives > 1)
            {
                rend.material.color = resouce.resouceHoverColor;
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
        rend.material.color = startColor;
    }
}
