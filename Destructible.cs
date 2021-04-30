using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä koodi toimii esineeseen liitettävänä, mistä tulee tuhoutuva, eli räjähtää pommista
public class Destructible : MonoBehaviour
{
    public GameObject explosionEffect;
    public int health;

    //Jos tuhottavan esineen elinaika menee nollalle, tuhottava esine räjähtää ja tuhoutuu
    void Update()
    {
        if (health <= 0f)
        {
            GameObject cloneExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            ParticleSystem.MainModule particle = cloneExplosion.GetComponent<ParticleSystem>().main;
            Destroy(gameObject);
            Destroy(cloneExplosion, 2);
        }
    }

    //Tuhottava ottaa vahinkoa tätä kutsumalla
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
