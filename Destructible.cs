using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will make an object destructible that it is attached to
public class Destructible : MonoBehaviour
{
    public GameObject explosionEffect;
    public int health;

    void Update()
    {
        //When the object's health goes to 0, the object will get destroyed and exploded
        if (health <= 0f)
        {
            GameObject cloneExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            ParticleSystem.MainModule particle = cloneExplosion.GetComponent<ParticleSystem>().main;
            Destroy(gameObject);
            Destroy(cloneExplosion, 2);
        }
    }

    //When calling this function, the object will take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
