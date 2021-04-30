using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä koodi saa kranaatin räjähtämään ja aiheuttamaan vahinkoa
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

    //Alkaessa koodi antaa kranaatille elinajan ja räjähdykselle viiveen heittäessä
    void Start()
    {
        countdown = delay;
        Invoke("Explode", lifeTime);
    }

    //Päivittäessä kranaatti räjähtää viiveen jälkeen
    void Update()
    {
        countdown -= Time.deltaTime;

        //Kun viive on ohi, tapahtuu räjähdys
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    //Kranaatin räjähtäessä kranaatti tuhoutuu ja aiheuttaa tietyille esineille
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        //Hakee lähistöllä olevia esineitä, joihin voi aiheuttaa vahinkoa
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            Barrel barrel = nearbyObject.GetComponent<Barrel>();
            EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();

            //Tuhottava esine ottaa vahinkoa
            if (dest != null)
            {
                dest.TakeDamage(damage);
            }

            //Räjähdetynnyri ottaa vahinkoa
            if (barrel != null)
            {
                barrel.TakeDamage(damage);
            }

            //Vihollinen ottaa vahinkoa
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
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

        //Tuhoaa objektin
        Destroy(gameObject);
    }
}
