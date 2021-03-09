using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    enum WorkerState
    {
        traveling,
        mining,
        recalling
    }

    public float speed = 12f;
    public int health = 10;

    private WorkerState currentState = WorkerState.traveling;
    private Resource resource;
    private Transform resourceTarget;
    private Transform home;
    private float detectionbuffer = 1.5f;

    private float mineTimer = 0f;

    public void SetResource(Resource source, Transform target, Transform homebase)
    {
        resource = source;
        resourceTarget = target;
        home = homebase;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == WorkerState.recalling)
        {
            Vector2 dir = home.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, home.position) <= detectionbuffer)
            {
                Debug.Log("Job Complete");
                PlayerStats.Lives++;
                Destroy(gameObject);
                return;
            }
        }

        if(currentState == WorkerState.traveling)
        {
            //create direction vector, translate normalized distance * speed * time, in world space
            Vector2 dir = resourceTarget.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, resourceTarget.position) <= detectionbuffer)
                currentState = WorkerState.mining;
        }

        if(currentState == WorkerState.mining)
        {
            if (resource.ResourceCheck())
            {
                if (mineTimer < resource.mineSpeed)
                    mineTimer += Time.deltaTime;
                else
                {
                    mineTimer = 0f;
                    resource.AddResource();
                }
            }
            else
                currentState = WorkerState.recalling;
        }
    }
}
