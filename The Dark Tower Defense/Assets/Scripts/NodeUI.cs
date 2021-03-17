using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text cost;
    public Text sellCost;
    public Button upgradeButton;

    private Node target;

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

    public void SelectTarget(Node node)
    {
        target = node;
        transform.position = target.offset.position;

        sellCost.text = "$" + target.turretBlueprint.GetSellAmount();

        if(!target.turretBlueprint.upgraded)
        {
            cost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            cost.text = "DONE";
            upgradeButton.interactable = false;
        }


        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
