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
