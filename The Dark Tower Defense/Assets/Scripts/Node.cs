using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Transform offset;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    //cacheing data
    private BuildManager buildManger;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManger = BuildManager.instance;
    }

    private void OnMouseDown()
    {
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
