using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Tämä koodi antaa pelaajalle elämän ja kuoleman
public class PlayerHealth : MonoBehaviour
{
    public int health;
    public AudioSource osumaAS;
    private CharacterController charcontr;
    public GameObject ragdollPrefab;
    public GameObject protodude;
    public GameObject deathMenu;
    public GameObject backgroundMusic;

    //Alkaessa pelaajalle asennetaan elämä täysille
    void Start()
    {
        charcontr = GetComponent<CharacterController>();
    }

    //Päivittäessä kun pelaajan vahinko menee nollalle, pelaaja kuolee
    void Update()
    {
        if (health <= 0)
        {
            Instantiate(ragdollPrefab, transform.position, protodude.transform.rotation);
            Destroy(gameObject);
            deathMenu.SetActive(true);
            backgroundMusic.SetActive(false);
        }
    }

    //Pelaaja ottaa vahinkoa vihollisilta tätä funktiota kutsumalla
    public void TakeDamage(int damage)
    {
        health -= damage;
        //healthBar.SetHealth(health);
    }

    //Pelaajaan osuessa vihollisen ampuma luoti tekee yhden vahingon
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ProjectileEnemy"))
        {
            TakeDamage(1);
            osumaAS.Play(0);
        }
    }
}
