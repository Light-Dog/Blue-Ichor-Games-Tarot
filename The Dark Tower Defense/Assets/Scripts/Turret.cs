using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Attributes")]
    public float range = 10f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float fireCountdown = 0f;

    [Header("Use Lazer")]
    public bool useLazer = false;

    public int damageOverTime = 30;
    public float slowPct = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactParticles;


    [Header("Set Up Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if(useLazer)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactParticles.Stop();
                }
            }

            return;
        }

        //Lock on Target
        partToRotate.LookAt(new Vector3(target.position.x, target.position.y, partToRotate.position.z));

        if (useLazer)
            Lazer();
        else
            FireBullet();


    }

    //----------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------
    
    //Can be upgraded to only search for a new target if it does not have one, based of the type of fire(first, last, strongest)
    //Update Target
    void UpdateTarget()
    {
        float shortestDistane = Mathf.Infinity;
        GameObject nearestEnemy = null;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistane)
            {
                shortestDistane = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistane <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
            target = null;
    }

    void FireBullet()
    {
        if (fireCountdown <= 0f)
        {
            //print("Shoot");
            GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            if (bullet)
                bullet.SeekTarget(target);

            fireCountdown = 1f / fireRate;
        }
        else
            fireCountdown -= Time.deltaTime;
    }

    void Lazer()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
            impactParticles.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 bounceDir = firePoint.position - target.position;

        impactParticles.transform.position = target.position + (bounceDir.normalized);

        //creates a vector pointing at bounce dir
        impactParticles.transform.rotation = Quaternion.LookRotation(bounceDir);
    }

    //----------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
