using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script will give the health for the player
public class PlayerHealth : MonoBehaviour
{
    public int health;
    public AudioSource osumaAS;
    private CharacterController charcontr;
    public GameObject ragdollPrefab;
    public GameObject protodude;
    public GameObject deathMenu;
    public GameObject backgroundMusic;

    void Start()
    {
        charcontr = GetComponent<CharacterController>();
    }

    void Update()
    {
        //When player's goes to 0, the player will die and game will be over
        if (health <= 0)
        {
            Instantiate(ragdollPrefab, transform.position, protodude.transform.rotation);
            Destroy(gameObject);
            deathMenu.SetActive(true);
            backgroundMusic.SetActive(false);
        }
    }

    //Player will take damage when calling this function
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        //When enemies shoot player, the player will receive one damage when enemies bullet hit player
        if (other.gameObject.CompareTag("ProjectileEnemy"))
        {
            TakeDamage(1);
            osumaAS.Play(0);
        }
    }
}
