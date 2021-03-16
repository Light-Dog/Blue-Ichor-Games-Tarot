using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject turretPrefab;
    public int cost = 0;

    public GameObject upgradedPrefab;
    public int upgradeCost = 0;
    public bool upgraded = false;
}
