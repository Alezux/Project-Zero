using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will give the actions to the bullets shot from the weapon
public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public int damage;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    //When a bullet enters the shootable area, the targets will respond to the bullet going in their area
    void OnTriggerEnter(Collider collider)
    {
        //This will cause one damage to all objects with an enemy tag and will destroy the object when health is 0
        if (collider.gameObject.tag == "Enemy")
        {
            collider.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(gameObject, 1.3f);
        }

        //This will cause one damage to all objects with a shootable tag, which also is the rest of objects that you can shoot and will destroy the object when health is 0
        if (collider.gameObject.tag == "Shootable")
        {
            collider.GetComponent<Shootable>().TakeDamage(damage);
            Destroy(gameObject, 0f);
        }

        //This will cause one damage to all objects with a barrel tag and will destroy the object when health is 0
        if (collider.gameObject.tag == "Barrel")
        {
            collider.GetComponent<Barrel>().TakeDamage(damage);
            Destroy(gameObject, 0f);
        }
    }
    
    //When shooting a bullet, the bullet's lifetime automatically will go to 0, which destroys the object after certain time. With this we can avoid the bullet being alive forever.
    void Update()
    {
        lifeTime -= Time.deltaTime;
    }

    //When calling this function, the bullet will get destroyed
    void DestroyProjectile()
    {
        Destroy(gameObject, lifeTime);
    }
}
