using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will give the enemies health
public class EnemyHealth : MonoBehaviour
{
    public GameObject deathSound;
    public GameObject enemy;
    public HealthBar healthBar;
    public int health;
    public GameObject ragdollPrefab;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        //When enemies health goes 0, the enemy will get destroyed
        if (health <= 0f)
        {
            deathSound.SetActive(true);
            Destroy(enemy);
            Instantiate(ragdollPrefab, this.transform.position, this.transform.rotation);
        }
    }

    //When calling this function, the enemy will take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }
}
