using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä koodi antaa räjähdetynnyrille toiminnat
public class Barrel : MonoBehaviour
{
    public GameObject explosionEffect;
    public int health;
    public float force;
    public float radius;
    public int damage;

    //Jos tuhottavan esineen elinaika menee nollalle, tuhottava esine tuhoutuu
    void Update()
    {
        if (health <= 0f)
        {
            Explode();
        }
    }

    //Tuhottava ottaa vahinkoa tätä kutsumalla
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    //Tämä funktio antaa räjähdyksen
    void Explode()
    {
        GameObject cloneExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        //Hakee lähistöllä olevia esineitä, joihin voi aiheuttaa vahinkoa
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
            Barrel barrel = nearbyObject.GetComponent<Barrel>();
            PlayerHealth player = nearbyObject.GetComponent<PlayerHealth>();
            Shootable shootable = nearbyObject.GetComponent<Shootable>();

            //Tuhottava esine ottaa vahinkoa
            if (dest != null)
            {
                dest.TakeDamage(damage);
            }

            //Vihollinen ottaa vahinkoa
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            //Räjähdetynnyri ottaa vahinkoa
            if (barrel != null)
            {
                barrel.TakeDamage(damage);
            }

            //Pelaaja ottaa vahinkoa
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            //Laatikot ottaa vahinkoa
            if (shootable != null)
            {
                shootable.TakeDamage(damage);
            }
        }

        //Valitsee colliderit
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        //Hakee lähistöllä olevia esineitä, joita voi siirtää
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            //Tasapainoiset esineet ottaa siirtovoiman vastaan
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        //Tuhoaa objektin ja räjähdykset
        Destroy(gameObject);
        Destroy(cloneExplosion, 2);
    }
}
