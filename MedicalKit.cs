using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä esine antaa lääkintäesineelle toiminnat
public class MedicalKit : MonoBehaviour
{
    public PlayerHealth playerHealth;

    //Alkaessa koodi etsii asioita
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        //Jos pelaajan elämä on täysillä, lääkintäesinettä ei voi kerätä
        if (playerHealth.health == 10)
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //Päivittäessä jos pelaajan elämä on vähentynyt, lääkintäesineen pystyy keräämään
    void Update()
    {
        if (playerHealth.health < 10)
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }

    //Lääkintäesineen alueelle osuessa pelaaja saa lääkintäesineestä lisää elämää, jos elämää on vähentynyt
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerHealth.health < 10)
        {
            Destroy(gameObject);
            playerHealth.health += 1;
            //playerHealth.healthBar.SetHealth(playerHealth.health);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
