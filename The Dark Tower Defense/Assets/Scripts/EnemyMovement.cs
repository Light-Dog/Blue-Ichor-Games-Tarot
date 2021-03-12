using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waveIndex = 0;
    private float detectionBuffer = .2f;

    private Enemy enemy;

    void Start()
    {
        //set inital target to point[0]
        target = Waypoints.points[0];
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        //create direction vector, translate normalized distance * speed * time, in world space
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        //if distance is less then detection buffer, get next waypoint, if final waypoint destroy the enemy
        if (Vector2.Distance(transform.position, target.position) <= detectionBuffer)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        waveIndex++;

        if (waveIndex == Waypoints.points.Length)
        {
            EndPath();
            return;
        }

        target = Waypoints.points[waveIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives -= 1;
        Destroy(gameObject);
    }
}
