using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public float explosionRadius = 0f;

    public int damage = 30;

    public GameObject impactEffect;

    private Transform target;

    public void SeekTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceTravel = speed * Time.deltaTime;

        if(dir.magnitude <= distanceTravel)
        {
            HitTarget();
            return;
        }
        else
        {
            transform.Translate(dir.normalized * distanceTravel, Space.World);
            transform.LookAt(target);
        }
    }

    void HitTarget()
    {
        if(impactEffect)
        {
            GameObject temp = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(temp, 1f);
        }

        if (explosionRadius > 0f)
        {
            Explosion();
        }
        else
            Damage(target);


        Destroy(gameObject);
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collide in colliders)
        {
            if (collide.tag == "Enemy")
                Damage(collide.transform);
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e)
            e.TakeDamage(damage);
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
