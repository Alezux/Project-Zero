using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will give the explosion to a grenade when it is thrown
public class GrenadeExplode : MonoBehaviour
{
    public bool hasExploded = false;
    public float countdown;
    public float delay = 1f;
    public float force = 700f;
    public float lifeTime;
    public float radius;
    public GameObject explosionEffect;
    public int damage;

    void Start()
    {
        countdown = delay;
        Invoke("Explode", lifeTime);
    }

    void Update()
    {
        //When the grenade is thrown, after certain time it will explode
        countdown -= Time.deltaTime;

        //When time is over, the greande exploses
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    //When calling this function, the grenade will explode
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        //This will reach all the objects that are near the grenade explosion that could take damage from grenade's explosion
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            Barrel barrel = nearbyObject.GetComponent<Barrel>();
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();

            //The destructible will take damage
            if (dest != null)
            {
                dest.TakeDamage(damage);
            }

            //The barrels will take damage
            if (barrel != null)
            {
                barrel.TakeDamage(damage);
            }

            //The enemies will take damage
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        //This will reach the objects nearby that could be moved, which means that when the barrel exploses close to them, they will move a little
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            //The objects will response to the gravity force
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        //At the last grenade will get destroyed
        Destroy(gameObject);
    }
}
