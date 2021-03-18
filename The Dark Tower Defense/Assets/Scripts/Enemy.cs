using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //speed, (transform)target, waveIndex, detection buffer for waypoint
    [HideInInspector]
    public float speed;
    private float health; 

    public float startSpeed = 20f;
    public float startHealth = 100;
    public int worth = 20;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = (health / startHealth);

        if (health <= 0)
            Die();
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        PlayerStats.Money += worth;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        Destroy(gameObject);
    }
}
