using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will make the objects shootable
public class Shootable : MonoBehaviour
{
    public GameObject destructionPrefab;
    public int health;

    void Update()
    {
        //When the object's health goes to zero, the object will get destroyed. The object can take damage from several things.
        if (health <= 0f)
        {
            Instantiate(destructionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    //This object will take damage when this function is called on the scripts that cause damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
