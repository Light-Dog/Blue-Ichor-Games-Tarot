using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public enum ResourceType
    {
        wood,
        stone,
        iron,
        empty
    }

    public Color resourceColor;
    public Color resouceHoverColor;
    public ResourceType resourceType = ResourceType.empty;

    public int resourceCount = 0;

    public float mineSpeed = 2f;
    public float bonusChance = .05f;

    public bool ResourceCheck() { return (resourceCount > 0); }
}
