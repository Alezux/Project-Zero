using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will give the actions for the collectable medical kits
public class MedicalKit : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        //When player's health is full, the player cannot collect medical kits
        if (playerHealth.health == 10)
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Update()
    {
        //When player has lost health, collecting medical kit will make the player receive one health more
        if (playerHealth.health < 10)
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //When player enters the medical kit's area and has lost health, the player will get health from collecting medical kit
        if (other.CompareTag("Player") && playerHealth.health < 10)
        {
            //Destroys medical kit after collecting
            Destroy(gameObject);

            //Player gets one health more
            playerHealth.health += 1;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
