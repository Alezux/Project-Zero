using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä koodi antaa viholliselle elinajan
public class EnemyHealth : MonoBehaviour
{
    public GameObject deathSound;
    public GameObject enemy;
    public HealthBar healthBar;
    public int health;
    public GameObject ragdollPrefab;

    //Alkaessa vihollinen saa elinajan määrän elinlinjalle
    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    //Päivittäessä, kun vihollisen elinaika menee nollalle, vihollinen tuhoutuu
    void Update()
    {
        if (health <= 0f)
        {
            deathSound.SetActive(true);
            Destroy(enemy);
            Instantiate(ragdollPrefab, this.transform.position, this.transform.rotation);
        }
    }

    //Vihollinen ottaa vahinkoa luodista
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }
}
