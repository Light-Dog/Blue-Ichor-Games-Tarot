using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public Enemy leftEnemy;
    public Enemy rightEnemy;

    private Quaternion target;

    private Quaternion left;
    private Quaternion right;

    public bool lookingLeft = true;

    public Transform partToRotate;
    public GameObject buildEffect;

    public float spinSpeed = 15f;

    private void Start()
    {
        Transform l = leftEnemy.transform;
        Transform r = rightEnemy.transform;

        partToRotate.LookAt(new Vector3(l.position.x, l.position.y, partToRotate.position.z));
        left = partToRotate.rotation;

        partToRotate.LookAt(new Vector3(r.position.x, r.position.y, partToRotate.position.z));
        right = partToRotate.rotation;

        target = left;

        GameObject effect = (GameObject)Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    private void Update()
    {
        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, target, Time.deltaTime * spinSpeed);
        
    }

    public void UpdateTower(bool left)
    {
        if (!lookingLeft && left)
            SwitchSides();
        else if (!left && lookingLeft)
            SwitchSides();
    }

    public void SwitchSides()
    {
        if (lookingLeft)
        {
            target = right;
            lookingLeft = false;
        }
        else
        {
            target = left;
            lookingLeft = true;
        }

        
    }
}
