using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;

    public void SelectTarget(Node node)
    {
        target = node;
        transform.position = target.offset.position;

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
