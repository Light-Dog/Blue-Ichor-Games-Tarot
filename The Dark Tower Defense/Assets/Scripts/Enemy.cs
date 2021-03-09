using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //speed, (transform)target, waveIndex, detection buffer for waypoint
    public float speed = 10f;
    public int health = 100;
    public int value = 20;

    public GameObject deathEffect;

    private Transform target;
    private int waveIndex = 0;
    private float detectionBuffer = .2f;

    void Start()
    {
        //set inital target to point[0]
        target = Waypoints.points[0];
    }

    void Update()
    {
        //create direction vector, translate normalized distance * speed * time, in world space
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //if distance is less then detection buffer, get next waypoint, if final waypoint destroy the enemy
        if(Vector2.Distance(transform.position, target.position) <= detectionBuffer)
        {
            GetNextWaypoint();
        }
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

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        PlayerStats.Money += value;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        Destroy(gameObject);
    }
}
