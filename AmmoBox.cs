using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script will give the actions for the ammoboxes
public class AmmoBox : MonoBehaviour
{
    private PlayerShooting playerShooting;
    public GameObject tutorialPrompt;

    void Start()
    {
        playerShooting = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerShooting>();

        //When the weapon has full ammo, the ammobox cannot be collected
        if (playerShooting.currentAmmo == 50)
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
    
    void Update()
    {
        //When the weapon has lost ammos, the ammoboxes can be collected
        if (playerShooting.currentAmmo < 50)
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //When player touches the ammobox, the player will get more ammo as long as there are ammos spent
        if (other.CompareTag("Player") && playerShooting.currentAmmo < 50)
        {
            //The ammobox will disappear after collecting
            Destroy(gameObject);

            //Ammo goes on max
            playerShooting.currentAmmo = playerShooting.maxAmmo;
            
            //Turning on and off
            this.GetComponent<BoxCollider>().enabled = false;
            playerShooting.canFire = true;
            tutorialPrompt.SetActive(true);
        }
    }
}
