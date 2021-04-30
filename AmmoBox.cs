using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tämä koodi antaa ammolaatikolle toiminnat
public class AmmoBox : MonoBehaviour
{
    private PlayerShooting playerShooting;
    public GameObject tutorialPrompt;

    //Alkaessa koodi hakee asioita
    void Start()
    {
        playerShooting = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerShooting>();

        //Jos aseella on ammukset täynnä, ammolaatikkoa ei voi kerätä
        if (playerShooting.currentAmmo == 50)
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
    
    //Päivittäessä jos aseen ammukset ovat vähentyneet, ammolaatikon pystyy keräämään
    void Update()
    {
        if (playerShooting.currentAmmo < 50)
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }

    //Ammolaatikon alueelle osuessa pelaaja saa aselle lisää ammuksia, jos ammuksia on vähentynyt
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerShooting.currentAmmo < 50)
        {
            Destroy(gameObject);
            playerShooting.currentAmmo = playerShooting.maxAmmo;
            this.GetComponent<BoxCollider>().enabled = false;
            playerShooting.canFire = true;
            tutorialPrompt.SetActive(true);
        }
    }
}
