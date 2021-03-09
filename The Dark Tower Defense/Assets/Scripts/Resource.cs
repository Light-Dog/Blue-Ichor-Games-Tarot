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

    public void AddResource()
    {
        resourceCount--;

        float chance = Random.Range(0f, 1f);
        Debug.Log("Bonus Chance: " + chance);

        switch (resourceType)
        {
            case ResourceType.wood:
                if (chance <= bonusChance)
                    PlayerStats.Wood += 3;
                PlayerStats.Wood++;
                break;
            case ResourceType.stone:
                if (chance <= bonusChance)
                    PlayerStats.Stone += 3;
                PlayerStats.Stone++;
                break;
            case ResourceType.iron:
                if (chance <= bonusChance)
                    PlayerStats.Iron += 3;
                PlayerStats.Iron++;
                break;
            case ResourceType.empty:
                break;
            default:
                break;
        }

    }
}
