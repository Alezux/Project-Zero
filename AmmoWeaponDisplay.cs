using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This code will show the amount of ammos on the hud of the game
public class AmmoWeaponDisplay : MonoBehaviour
{
    public PlayerShooting weapon;
    public Text ammoWeaponDisplay;

    //Alkaessa koodi hakee pistoolista toiminnot, jotta saadaan pelin hudille näkymään pistoolin ammusten määrä
    void Start()
    {
        //This will get the hud text increase and decrease the amount of ammos based on how many ammos player has
        weapon = GetComponentInChildren<PlayerShooting>();

        //These will insert the ammo text on the hud when the player has collected weapon
        ammoWeaponDisplay = GameObject.FindGameObjectWithTag("AmmoWeaponDisplay").GetComponent<Text>();
        ammoWeaponDisplay.enabled = false;
    }

    //This will give the text about the player's ammos
    void Update()
    {
        ammoWeaponDisplay.text = weapon.currentAmmo + " / " + weapon.maxAmmo;
    }
}