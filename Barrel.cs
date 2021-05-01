using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will give the actions for explosive barrel gameobjects
public class Barrel : MonoBehaviour
{
    public GameObject explosionEffect;
    public int health;
    public float force;
    public float radius;
    public int damage;

    //When the barrel's health goes 0, the object will get destroyed, in this case it will explode as well
    void Update()
    {
        if (health <= 0f)
        {
            Explode();
        }
    }

    //When calling this function, the barrel will take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    //When calling this function, the barrel will explode and get destroyed
    void Explode()
    {
        GameObject cloneExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        //This will reach all the objects that are near the barrel that could take damage from barrel's explosion
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
            Barrel barrel = nearbyObject.GetComponent<Barrel>();
            PlayerHealth player = nearbyObject.GetComponent<PlayerHealth>();
            Shootable shootable = nearbyObject.GetComponent<Shootable>();

            //The rest of all objects in the game will take damage when they are destroyable
            if (dest != null)
            {
                dest.TakeDamage(damage);
            }

            //The enemies will take damage
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            //The other barrels will take damage
            if (barrel != null)
            {
                barrel.TakeDamage(damage);
            }

            //The player will take damage
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            //The shootable objects will take damage, in this case they are boxes
            if (shootable != null)
            {
                shootable.TakeDamage(damage);
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

        //This will destroy the object when everything has happened in the script below
        Destroy(gameObject);
        Destroy(cloneExplosion, 2);
    }
}
