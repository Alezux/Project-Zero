using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script will give more features to player's shooting
public class PlayerShooting : MonoBehaviour
{
    public GameObject ammoDisplay;
    private GameObject cursor;
    public GameObject weaponIcon;
    public Rigidbody projectilePrefab;
    public Transform spawnPoint;
    public float fireRate = 0.2f;
    private float nextFire = 0.0f;
    public int currentAmmo;
    public int maxAmmo;
    public bool canFire;
    private Animator anim;

    void Start()
    {
        currentAmmo = maxAmmo;
        canFire = true;
    }

    public void Update()
    {
        //Always when player is able to shoot, the script will work. When the canfire bool is turned off, the player is not able to shoot
        if (canFire)
        {
            //This will give the player shooting animation when they are able to shoot
            anim = GetComponentInParent<Animator>();

            //This will give the right direction where the bullets start shooting from and where to
            this.transform.rotation = spawnPoint.transform.rotation;

            //When pressing the button, the player can shoot
            if (Input.GetButtonDown("Shoot") && Time.time > nextFire)
            {
                //Sets the shooting animation to player
                anim.SetTrigger("Shoot");

                //These will give timings when you can shoot the next bullet
                Invoke("Shoot", 0.1f);
                nextFire = Time.time + fireRate;

                //This will make the weapon lose ammos
                currentAmmo--;
            }

            //This will shut off the ability to shoot when the ammos are 0
            if (currentAmmo <= 0)
            {
                currentAmmo = 0;
                canFire = false;
            }

            //This will make you able to shoot when you have more than 0 ammos
            if (currentAmmo > 0)
            {
                canFire = true;
            }
        }
    }

    //When calling this function, the player is able to shoot again
    void Shoot()
    {
        Rigidbody hitPlayer;
        hitPlayer = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
        hitPlayer.velocity = spawnPoint.transform.forward * 100f;
    }
}