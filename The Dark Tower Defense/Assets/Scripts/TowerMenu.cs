using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    public Enemy leftEnemy;
    public Enemy rightEnemy;

    private Quaternion target;

    private Quaternion left;
    private Quaternion right;

    public bool lookingLeft = true;

    public Transform partToRotate;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SwitchSides();

        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, target, Time.deltaTime * spinSpeed);
        
    }

    void SwitchSides()
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
